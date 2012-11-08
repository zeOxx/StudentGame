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
        private Menu menu;

        public Controller(ContentHolder content, SoundManager soundManager)
        {
            //playerObject = new GameObject[Constants.maxNumberOfObjectsInArray];
            this.soundManager = soundManager;
            playerObject = null;
            this.content = content;
            physicsEngine = new PhysicsEngine();
        }

        //************** FUNCTIONS ******************
        
        public Gameworld updateGameWorld(Gameworld gameWorld, GameTime gameTime, InputHandler inputHandler)
        {
            getModelFromGameworld(gameWorld);

            //Change gamestate

            checkInput(gameTime, inputHandler, gameWorld);
            gameWorld.updateEnemies(gameTime);
            handleProjectileCollisions(gameWorld);
            

            //physicsEngine.collisionDetection(gameWorld);

            gameWorld.setPlayer(playerObject);

            gameWorld.update(gameTime, content);
            playSounds();

            return gameWorld;
        }

        public void updateMenu(InputHandler inputHandler, GameTime gameTime)
        {
            Vector2 leftStick = inputHandler.getLeftStickMovement();
            bool aButton = false;                       // flagged if A button is pressed

            if (inputHandler.ButtonReleased(Buttons.A))
                aButton = true;

            menu.update(leftStick, aButton, gameTime);
        }

        public void handleProjectileCollisions(Models.Gameworld gameWorld)
        {
            //List<int> projectileCollisions = physicsEngine.getProjectileCollisions();

            for (int i = 0; i < physicsEngine.getProjectileCollisions().Count; i++)
            {
                playerObject.getBulletList().RemoveAt(physicsEngine.getProjectileCollisions()[i]);
                //projectileCollisions.RemoveAt(i);
                //Console.WriteLine("Removed!");
            }

            List<Models.Collidable> rocks = gameWorld.getRock();
            //for (int i = 0; i < physicsEngine.getRockCollisions().Count; i++)
            //{
              //  gameWorld.getRock().RemoveAt(physicsEngine.getRockCollisions()[i]);
                //projectileCollisions.RemoveAt(i);
                //Console.WriteLine("Removed!");
            //}

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
                soundManager.playGunSound();
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
