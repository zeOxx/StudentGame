using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
         
            Vector2 prePosition = playerObject.getPosition();
            Vector2 preVelocity = playerObject.getVelocity();
 
            playerObject.update(movementVector, rightStick, gameTime);

            Vector2 postPosition = playerObject.getPosition();
            Vector2 postVelocity = playerObject.getVelocity();
 
  
            for (int i = 0; i < rocks.Count(); i++)
            {
                if (rocks[i].BoundingBox.Intersects(playerObject.BoundingBox))
                {
                    //Console.Out.WriteLine("CRAAAAASH!!");

                    if (perPixelCollisionDetection(playerObject, rocks[i]))
                    {
                        playerObject.setPosition(prePosition.X, prePosition.Y);
                        playerObject.setVelocity(Vector2.Zero);

                        //playerObject.setPosition(0, 0);
                        //Console.Out.WriteLine("COOLOOOORR");
                        //playerObject.setPosition(position.X, position.Y);
                    }
                   

                    //Vector2 velocity = playerObject.getVelocity();
                    //playerObject.setPosition(position.X - velocity.X, position.Y - velocity.Y);
                    //playerObject.setVelocity(Vector2.Zero);


                    //Console.Out.WriteLine((position.X - velocity.X) + " " + (position.Y - velocity.Y));
                }
            }
<<<<<<< HEAD
            
            //If player reaches end of screen - velocity set to 0 -
=======
>>>>>>> cf2dbe7f88947e9c0473f29f48cb38a8291dba6d
            if (!playerObject.BoundingBox.Intersects(gameWorld.LevelBounds))
            {
                playerObject.setPosition(prePosition.X, prePosition.Y);
                playerObject.setVelocity(Vector2.Zero);    
            }

        }

        public bool perPixelCollisionDetection(Models.Player playerObject, Models.GameObject collidingObject)
        {
            int top = Math.Max(playerObject.BoundingBox.Top, collidingObject.BoundingBox.Top);
            int bottom = Math.Min(playerObject.BoundingBox.Bottom, collidingObject.BoundingBox.Bottom);
            int left = Math.Max(playerObject.BoundingBox.Left, collidingObject.BoundingBox.Left);
            int right = Math.Min(playerObject.BoundingBox.Right, collidingObject.BoundingBox.Right);

            Vector2 position = playerObject.getPosition();

            Texture2D playerTexture = playerObject.getTexture();
            Texture2D collidableTexture = collidingObject.getTexture();

            Color[] playerTextureData = new Color[playerTexture.Width * playerTexture.Height];
            Color[] collidableTextureData = new Color[collidableTexture.Width * collidableTexture.Height];

            playerTexture.GetData(playerTextureData);
            collidableTexture.GetData(collidableTextureData);
            Vector2 velocity = playerObject.getVelocity();

            for (int y = top; y < bottom; y++)
            {
                int rowOffsetPlayer =  (y - playerObject.BoundingBox.Top)    * playerObject.BoundingBox.Width;
                int rowOffsetCollide = (y - collidingObject.BoundingBox.Top) * collidingObject.BoundingBox.Width;
                for (int x = left; x < right; x++)
                {
                
                    // Get the color of both pixels at this point
                    Color colorA = playerTextureData[rowOffsetPlayer +
                                               (x - playerObject.BoundingBox.Left)
                                        ];
                    Color colorB = collidableTextureData[rowOffsetCollide +
                                                (x - collidingObject.BoundingBox.Left)
                                         ];
 
                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                    
                }   
            }
            
                    
             return false;
                    
        }
    }
}
