using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;

namespace Perihelion.Controllers
{
    class PhysicsEngine
    {
        private List<Models.Collidable> rocks;
        private Models.Player playerObject;
        private ArrayList collisions;

        public PhysicsEngine()
        {
            rocks = new List<Models.Collidable>();
            collisions = new ArrayList();
        }

        public void collisionDetection(Models.Gameworld gameWorld, Vector2 movementVector, Vector2 rightStick, GameTime gameTime)
        {
            rocks = gameWorld.getRock();
            playerObject = gameWorld.getPlayer();
            Vector2 position = playerObject.getPosition();

            playerObject.update(movementVector, rightStick, gameTime);

            for (int i = 0; i < rocks.Count(); i++)
            {
                if (rocks[i].BoundingBox.Intersects(playerObject.BoundingBox))
                {
                    Console.Out.WriteLine("CRAAAAASH!!");

                    playerObject.setPosition(position.X, position.Y);

                    //Vector2 velocity = playerObject.getVelocity();
                    //playerObject.setPosition(position.X-velocity.X, position.Y-velocity.Y);
                    //Console.Out.WriteLine((position.X - velocity.X) + " " + (position.Y - velocity.Y));
                }
                
            }

        }
    }
}
