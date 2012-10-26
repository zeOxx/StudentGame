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

            //physicsEngine.collisionDetection(gameWorld);

            gameWorld.setPlayer(playerObject);

            gameWorld.update();
            playSounds();

            return gameWorld;
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

        // Checks input
        //Gameworld as argument is JUST FOR TESTING-PURPOSES
        public void checkInput(GameTime gameTime, InputHandler inputHandler, Gameworld gameWorld)
        {
            // Controller input
            Vector2 movementVector = inputHandler.getMovementInputFromPlayer();
            Vector2 rightStick = inputHandler.getShootingInputFromPlayer();
            //playerObject.update(movementVector, rightStick, gameTime);

            physicsEngine.collisionDetection(gameWorld, movementVector, rightStick, gameTime);

            

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
