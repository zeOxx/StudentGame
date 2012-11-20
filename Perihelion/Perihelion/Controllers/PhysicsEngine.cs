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
        //private List<Models.Collidable> rocks;
        //private Models.Player playerObject;
        //private ArrayList collisions;
        private List<int> projectileCollisionsIndex;
        private List<int> rockCollisionIndex;
        private List<int> enemyCollisionIndex;

        //private List<int> intList;
        static private List<Models.GameObject> collisions;

        

        public PhysicsEngine()
        {
            //rocks = new List<Models.Collidable>();
            //collisions = new ArrayList();
            //projectileCollisionsList = new List<Models.GameObject>();
            projectileCollisionsIndex = new List<int>();
            rockCollisionIndex = new List<int>();
            enemyCollisionIndex = new List<int>();
            collisions = new List<Models.GameObject>();

        }

        public void collisionDetection(ref Models.Gameworld gameWorld, Vector2 movementVector, Vector2 rightStick, GameTime gameTime)
        {
            //playerObject.update(movementVector, rightStick, gameTime);

            rockCollisionIndex.Clear();
            projectileCollisionsIndex.Clear();
            enemyCollisionIndex.Clear();
            collisions.Clear();
            
            //rocks = gameWorld.getRock();
            //playerObject = gameWorld.getPlayer();

            projectileCollisions(gameWorld);
            playerCollisions(gameWorld, movementVector, rightStick, gameTime);
            levelBoundCollision(gameWorld);
        }

        public List<Models.GameObject> getCollisions()
        {
            return collisions;
        }

        public List<int> getRockCollisions()
        {
            return rockCollisionIndex;
        }

        public List<int> EnemyCollision
        {
            get { return enemyCollisionIndex; }
        }


        private void projectileCollisions(Models.Gameworld gameWorld)
        {
            //List<Models.Projectile> bullets = gameWorld.getPlayer().getBulletList();

            List<int> collidedProjectileIndexes = new List<int>();



            //Console.WriteLine("Lengde: " + playerObject.getBulletList().Count);

            for (int i = 0; i < gameWorld.getPlayer().BulletList.Count; i++)
            {
                //Vector2 bulletPrePosition = gameWorld.getPlayer().getBulletList()[i].getPosition();
                for (int j = 0; j < gameWorld.getRock().Count; j++)
                //foreach (Models.Collidable rock in gameWorld.getRock())
                {
                    if(gameWorld.getRock()[j].BoundingBox.Intersects(gameWorld.getPlayer().BulletList[i].BoundingBox))
                    //if (gameWorld.getPlayer().getBulletList()[i].BoundingBox.Intersects(gameWorld.getRock()[j].BoundingBox))
                    {
                        if (perPixelCollisionDetection(gameWorld.getPlayer().BulletList[i],
                                                        gameWorld.getRock()[j]))
                        {
                            collidedProjectileIndexes.Add(i);
                            rockCollisionIndex.Add(j);
                            gameWorld.getRock()[j].updateCurrentHealth(-(gameWorld.getPlayer().BulletList[i].Damage));
                            collisions.Add(gameWorld.getPlayer().BulletList[i]);
                            collisions.Add(gameWorld.getRock()[j]);

                            //Console.WriteLine("KABLAAAM!!");
                        }
                    }
                }

                //for (int l = 0; l < gameWorld.EnemyList.Count; l++)
                //{
                //    if (gameWorld.EnemyList[l].BoundingBox.Intersects(gameWorld.getPlayer().BulletList[i].BoundingBox))
                //    //if (gameWorld.getPlayer().getBulletList()[i].BoundingBox.Intersects(gameWorld.getRock()[j].BoundingBox))
                //    {
                //        if (perPixelCollisionDetection(gameWorld.getPlayer().BulletList[i],
                //                                        gameWorld.EnemyList[l]))
                //        {
                //            collidedProjectileIndexes.Add(i);
                //            enemyCollisionIndex.Add(l);
                //            gameWorld.EnemyList[l].updateCurrentHealth(-(gameWorld.getPlayer().BulletList[i].Damage));
                //            collisions.Add(gameWorld.getPlayer().BulletList[i]);
                //            collisions.Add(gameWorld.EnemyList[l]);

                //            //Console.WriteLine("KABLAAAM!!");
                //        }
                //    }
                //}
            }

            //Remove collided projectiles. 
            if (collidedProjectileIndexes.Count > 0)
            {
                for (int i = 0; i < collidedProjectileIndexes.Count; i++)
                {
                    gameWorld.getPlayer().BulletList.RemoveAt(collidedProjectileIndexes[i]);
                }
            }
        }

        private void playerCollisions(Models.Gameworld gameWorld, Vector2 movementVector, Vector2 rightStick, GameTime gameTime)
        {
            Vector2 prePosition = gameWorld.getPlayer().Position;


            gameWorld.getPlayer().update(movementVector, rightStick, gameTime);

            //for (int i = 0; i < gameWorld.getRock().Count(); i++)
            foreach (Models.Collidable rock in gameWorld.getRock())
            {
                if (rock.BoundingBox.Intersects(gameWorld.getPlayer().BoundingBox))
                {
                    if (perPixelCollisionDetection(gameWorld.getPlayer(), rock))
                    {
                        gameWorld.getPlayer().Position = new Vector2(prePosition.X, prePosition.Y);
                        gameWorld.getPlayer().Velocity = (Vector2.Zero);
                        
                    }
                }
            }
            
            //If player reaches end of screen - velocity set to 0 -
        }

        private void levelBoundCollision(Models.Gameworld gameWorld)
        {
            Vector2 prePosition = gameWorld.getPlayer().Position;
            if (!gameWorld.getPlayer().BoundingBox.Intersects(gameWorld.LevelBounds))
            {
                gameWorld.getPlayer().Position = new Vector2(prePosition.X, prePosition.Y);
                gameWorld.getPlayer().Velocity = Vector2.Zero;
            }
        }


        // Checks for per pixel collision between two objects.
        private bool perPixelCollisionDetection(Models.GameObject playerObject, Models.GameObject collidingObject)
        {
            int top = Math.Max(playerObject.BoundingBox.Top, collidingObject.BoundingBox.Top);
            int bottom = Math.Min(playerObject.BoundingBox.Bottom, collidingObject.BoundingBox.Bottom);
            int left = Math.Max(playerObject.BoundingBox.Left, collidingObject.BoundingBox.Left);
            int right = Math.Min(playerObject.BoundingBox.Right, collidingObject.BoundingBox.Right);

            Vector2 position = playerObject.Position;

            Texture2D playerTexture = playerObject.Texture;
            Texture2D collidableTexture = collidingObject.Texture;

            Color[] playerTextureData = new Color[playerTexture.Width * playerTexture.Height];
            Color[] collidableTextureData = new Color[collidableTexture.Width * collidableTexture.Height];

            playerTexture.GetData(playerTextureData);
            collidableTexture.GetData(collidableTextureData);

            Vector2 velocity = playerObject.Velocity;

            for (int y = top; y < bottom; y++)
            {
                int rowOffsetPlayer = (y - playerObject.BoundingBox.Top) * playerObject.BoundingBox.Width;
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
