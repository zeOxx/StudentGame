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
        //private Projectile bullets;

        public Controller()
        {
            //playerObject = new GameObject[Constants.maxNumberOfObjectsInArray];
            playerObject = null;
        }

        //************** FUNCTIONS ******************
        
        public Gameworld updateGameWorld(Gameworld gameWorld, GameTime gameTime, InputHandler inputHandler)
        {
            getModelFromGameworld(gameWorld);

            //Change gamestate

            checkInput(gameTime, inputHandler, gameWorld);

            gameWorld.setPlayer(playerObject);

            gameWorld.update();

            return gameWorld;
        }

        //Copies the entire Gamestate
        public void getModelFromGameworld(Gameworld gameWorld)
        {
            playerObject = gameWorld.getPlayer();
        }

        // Checks input
        //Gameworld as argument is JUST FOR TESTING-PURPOSES
        public void checkInput(GameTime gameTime, InputHandler inputHandler, Gameworld gameWorld)
        {
            //playerObject.updateVelocity(inputHandler.getMovementInputFromPlayer());
            //playerObject.updatePosition();

            Vector2 movementVector = inputHandler.getMovementInputFromPlayer();
            Vector2 rightStick = inputHandler.getShootingInputFromPlayer();
            playerObject.update(movementVector, rightStick);


            //Temp input
            inputHandler.updateInput();

            if (inputHandler.KeyDown(Keys.X))
            {
                gameWorld.getCamera().Zoom += (float)0.01;
            }

            if (inputHandler.KeyDown(Keys.Z))
            {
                gameWorld.getCamera().Zoom -= (float)0.01;
            }
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
        }

//         public void updateBullets(Vector2 motion)
//         {
// 
//         }
    }
}
