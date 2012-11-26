using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Perihelion.Models;
using Perihelion;


namespace Perihelion.Controllers
{
    class Controller
    {
        
        //************** VARIABLES ******************
        private Player playerObject;
        private ContentHolder content;
        private SoundManager soundManager;
        private PhysicsEngine physicsEngine;
        


        public Controller(ContentHolder content, SoundManager soundManager, string title)
        {
            //playerObject = new GameObject[Constants.maxNumberOfObjectsInArray];
            this.soundManager = soundManager;
            playerObject = null;
            this.content = content;
            physicsEngine = new PhysicsEngine();
            
        }

        //************** FUNCTIONS ******************
        
        public void updateGameWorld(ref Gameworld gameWorld, GameTime gameTime, InputHandler inputHandler)
        {
            getModelFromGameworld(gameWorld);

            //Change gamestate

            checkInput(gameTime, inputHandler, gameWorld);
            gameWorld.updateEnemies(gameTime);
            handleProjectileCollisions(gameWorld);
            updateGravityWell(gameWorld);

            //physicsEngine.collisionDetection(gameWorld);

            gameWorld.setPlayer(playerObject);

            gameWorld.update(gameTime, content);
            playSounds();

            //return gameWorld;
        }

        public void updateGravityWell(Gameworld gameWorld)
        {
            gameWorld.GravityWell.Position = gameWorld.PlayerObject.Position;
            //System.Console.WriteLine("X = " + gameWorld.GravityWell.Position.X + " Y = " + gameWorld.GravityWell.Position.Y);
        }

        public Menu updateMenu(Menu menu, InputHandler inputHandler, GameTime gameTime)
        {
            inputHandler.updateInput();

            Vector2 movementVector = inputHandler.getLeftStickMovement();

            bool aButton = false;                       // flagged if A button, or Enter, is pressed
            bool bButton = false;                       // flagged if B button, or backspace, is pressed

            int movement = 0;                           // Tells the menu where to move next (-1 is up, 1 is down)

            if (inputHandler.ButtonPressed(Buttons.DPadDown) || movementVector.Y < 0 || inputHandler.KeyReleased(Keys.Down))
                movement = 1;
            if (inputHandler.ButtonPressed(Buttons.DPadUp) || movementVector.Y > 0 || inputHandler.KeyReleased(Keys.Up))
                movement = -1;

            if (inputHandler.ButtonPressed(Buttons.A) || inputHandler.KeyDown(Keys.Enter))
                aButton = true;
            if (inputHandler.ButtonPressed(Buttons.B) || inputHandler.KeyDown(Keys.Back))
                bButton = true;

            menu.update(movement, aButton, bButton, gameTime);

            return menu;
        }

        
        
        public void handleProjectileCollisions(Models.Gameworld gameWorld)
        {
            List<int> rockCollisionsIndex                       = physicsEngine.RockCollisionIndexes;
            List<int> enemyCollisionsIndex                      = physicsEngine.EnemyCollisionIndexes;
            List<Models.GameObject> rockProjectileCollisions    = physicsEngine.getRockProjectileCollisions();
            List<Models.GameObject> enemyProjectileCollisions   = physicsEngine.getEnemyProjectileCollisions();
            Models.Projectile projectile                        = null;
            
            int damage;

            if (rockProjectileCollisions.Count != 0)
            {
                for (int i = 0; i < rockProjectileCollisions.Count; i++)
                {
                    projectile = (Models.Projectile)rockProjectileCollisions[i++];
                    damage = projectile.Damage;

                    rockProjectileCollisions[i].updateCurrentHealth(-damage);

 
                }
                for (int i = 0; i < rockCollisionsIndex.Count; i++)
                {
                    if (gameWorld.getRock()[rockCollisionsIndex[i]].CurrentHealth <= 0)
                    {
                        spawnExplosionParticles(gameWorld.getRock()[rockCollisionsIndex[i]], projectile, gameWorld.getParticleSystem());
                        soundManager.playSound("explosion");
                        gameWorld.getRock().RemoveAt(rockCollisionsIndex[i]);
                    }
                }
            }

            if(enemyProjectileCollisions.Count != 0)
            {
                for (int i = 0; i < enemyProjectileCollisions.Count; i++)
                {
                    projectile = (Models.Projectile)enemyProjectileCollisions[i];
                    i++;
                    damage = projectile.Damage;

                    enemyProjectileCollisions[i].updateCurrentHealth(-damage);

                    
                }
                for (int i = enemyCollisionsIndex.Count -1; i >= 0; i--)
                {
                    if (gameWorld.EnemyList[enemyCollisionsIndex[i]].CurrentHealth <= 0)
                    {
                        spawnExplosionParticles(gameWorld.EnemyList[enemyCollisionsIndex[i]], projectile, gameWorld.getParticleSystem());
                        soundManager.playSound("explosion");
                        gameWorld.EnemyList.RemoveAt(enemyCollisionsIndex[i]);
                    }
                }
            }
        }

        //Creates explosion 
        public void spawnExplosionParticles(Models.GameObject explodingObject, Models.Projectile projectile, Controllers.ParticleSystem particleSystem)
        {
            Vector2 objectPosition = explodingObject.Position;
            Vector2 explosionDirection = projectile.Velocity;
            particleSystem.newSpawner(content.particle_test, objectPosition, 1000, 0, 10, false, explosionDirection*7);
        }
        
        //Copies the entire Gamestate
        public void getModelFromGameworld(Gameworld gameWorld)
        {
            playerObject = gameWorld.getPlayer();
        }


        public void playSounds()
        {
            if (playerObject.BulletMade)
            {
                soundManager.playSound("pang");
            }
        }

        // Checks input during gameplay
        //Gameworld as argument is JUST FOR TESTING-PURPOSES
        public void checkInput(GameTime gameTime, InputHandler inputHandler, Gameworld gameWorld)
        {
            // Controller input
            Vector2 movementVector = inputHandler.getLeftStickMovement();
            Vector2 rightStick = inputHandler.getRightStickMovement();
            //playerObject.update(movementVector, rightStick, gameTime);

            
            physicsEngine.collisionDetection(ref gameWorld, movementVector, rightStick, gameTime);

            

#if DEBUG
            if (inputHandler.ButtonPressed(Buttons.LeftShoulder) && inputHandler.ButtonPressed(Buttons.RightShoulder))
                gameWorld.setDebug();
#endif
           

            // Temp Keyboardinput
            inputHandler.updateInput();

            // Zooming
            if (inputHandler.KeyDown(Keys.X))
            {
                gameWorld.getCamera().Zoom += 0.01f;
            }
            if (inputHandler.KeyDown(Keys.Z))
            {
                gameWorld.getCamera().Zoom -= 0.01f;
            }
            if (inputHandler.KeyDown(Keys.C))
            {
                gameWorld.getCamera().Zoom = 1.0f;
            }

            // Movement
            if (inputHandler.KeyDown(Keys.D))
            {
                playerObject.updatePosition(5, 0);
            }

            if (inputHandler.KeyDown(Keys.A))
            {
                playerObject.updatePosition(-5, 0);

            }

            if (inputHandler.KeyDown(Keys.S))
            {
                playerObject.updatePosition(0, 5);
            }


            if (inputHandler.KeyDown(Keys.W))
            {
                playerObject.updatePosition(0, -5);

            }

            // Other keys
#if DEBUG
            if (inputHandler.KeyReleased(Keys.F1))
                gameWorld.setDebug();
#endif
        }
    }
}
