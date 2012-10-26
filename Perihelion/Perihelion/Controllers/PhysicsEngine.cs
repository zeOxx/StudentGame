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
        private List<Models.GameObject> projectileCollisionsList;
        //private List<Models.GameObject> playerCollisions;

        public PhysicsEngine()
        {
            rocks = new List<Models.Collidable>();
            collisions = new ArrayList();
            projectileCollisionsList = new List<Models.GameObject>();
        }

        public void collisionDetection(ref Models.Gameworld gameWorld, Vector2 movementVector, Vector2 rightStick, GameTime gameTime)
        {
            //playerObject.update(movementVector, rightStick, gameTime);
            projectileCollisionsList.Clear();

            rocks = gameWorld.getRock();
            playerObject = gameWorld.getPlayer();

            projectileCollisions(playerObject, gameWorld);
            playerCollisions(playerObject, gameWorld, movementVector, rightStick, gameTime);
            levelBoundCollision(playerObject, gameWorld);
        }

        public List<Models.GameObject> getProjectileCollisions()
        {
            return projectileCollisionsList;
        }


        private void projectileCollisions(Models.Player playerObject, Models.Gameworld gameWorld)
        {
            List<Models.Projectile> bullets = playerObject.getBulletList();


            Console.WriteLine("Lengde: " + playerObject.getBulletList().Count);

            for (int i = 0; i < playerObject.getBulletList().Count; i++)
            {
                for (int j = 0; j < rocks.Count; j++)
                {
                    if (bullets[i].BoundingBox.Intersects(rocks[j].BoundingBox))
                    {
                        if (perPixelCollisionDetection(playerObject.getBulletList()[i], rocks[j]))
                        {
                            Console.WriteLine("HIT!!!!!");
                            //projectileCollisionsList.Add(bullets[i]);
                            //projectileCollisionsList.Add(rocks[j]);
                            playerObject.getBulletList().RemoveAt(i);

                        }
                    }
                }
            }

        }

        private void playerCollisions(Models.Player playerObject, Models.Gameworld gameWorld, Vector2 movementVector, Vector2 rightStick, GameTime gameTime)
        {
            Vector2 prePosition = playerObject.getPosition();
            

            playerObject.update(movementVector, rightStick, gameTime);

            for (int i = 0; i < rocks.Count(); i++)
            {
                if (rocks[i].BoundingBox.Intersects(playerObject.BoundingBox))
                {
                    if (perPixelCollisionDetection(playerObject, rocks[i]))
                    {
                        playerObject.setPosition(prePosition.X, prePosition.Y);
                        playerObject.setVelocity(Vector2.Zero);
                    }
                }
            }
        }

        private void levelBoundCollision(Models.Player playerObject, Models.Gameworld gameWorld)
        {
            Vector2 prePosition = playerObject.getPosition();
            if (!playerObject.BoundingBox.Intersects(gameWorld.LevelBounds))
            {
                playerObject.setPosition(prePosition.X, prePosition.Y);
                playerObject.setVelocity(Vector2.Zero);
            }
        }

        private bool perPixelCollisionDetection(Models.GameObject playerObject, Models.GameObject collidingObject)
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
