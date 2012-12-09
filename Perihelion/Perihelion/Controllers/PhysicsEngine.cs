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
        private List<int> projectileCollisionsIndex;
        private List<int> rockCollisionIndex;
        private List<int> enemyCollisionIndex;
        static private List<Models.GameObject> rockProjectileCollisions;
        static private List<Models.GameObject> enemyProjectileCollisions;
        static private List<Models.GameObject> playerProjectileCollisions;


        public PhysicsEngine()
        {
            projectileCollisionsIndex   = new List<int>();
            rockCollisionIndex          = new List<int>();
            enemyCollisionIndex         = new List<int>();
            rockProjectileCollisions    = new List<Models.GameObject>();
            enemyProjectileCollisions   = new List<Models.GameObject>();
            playerProjectileCollisions  = new List<Models.GameObject>();
        }

        public void collisionDetection(ref Models.Gameworld gameWorld, Vector2 movementVector, Vector2 rightStick, GameTime gameTime)
        {
            rockCollisionIndex.Clear();
            rockProjectileCollisions.Clear();

            enemyCollisionIndex.Clear();
            enemyProjectileCollisions.Clear();

            playerProjectileCollisions.Clear();
            projectileCollisionsIndex.Clear();
            checkPlayerProjectileCollisions(gameWorld);

            checkEnemyProjectileCollisions(gameWorld);
            
            playerCollisions(gameWorld, movementVector, rightStick, gameTime);
        }

        public List<Models.GameObject> getRockProjectileCollisions()
        {
            return rockProjectileCollisions;
        }

        public List<Models.GameObject> getEnemyProjectileCollisions()
        {
            return enemyProjectileCollisions;
        }

        public List<int> RockCollisionIndexes
        {
            get { return rockCollisionIndex; }
        }

        public List<int> EnemyCollisionIndexes
        {
            get { return enemyCollisionIndex; }
        }

        public void checkGravityWellCollision(Models.GameObject gravityWell, List<Models.Collidable> rocks)
        {
            for(int i = 0; i < rocks.Count; i++)
            {
                if (gravityWell.BoundingBox.Intersects(rocks[i].BoundingBox))
                {
                    if (perPixelCollisionDetection(gravityWell, rocks[i]))
                    {
                        calculateGravitationalPull(gravityWell, rocks[i]);
                    }
                }
            }
        }

        /*
         * Applies a gravitational pull on the second parameter object. Mass is hardcoded, but 
         * this can be changed. IT WORKS!!
         */
        private void calculateGravitationalPull(Models.GameObject gravityWell, Models.Collidable rock)
        {
            float distance = Math.Abs((float) Math.Sqrt(((rock.Position.X - gravityWell.Position.X) *
                                                (rock.Position.X - gravityWell.Position.X))
                                                           +
                                               ((rock.Position.Y - gravityWell.Position.Y) *
                                                (rock.Position.Y - gravityWell.Position.Y))
                                       ));

            const int mass = 15;


            float deltaX = rock.Position.X - gravityWell.Position.X;
            float deltaY = rock.Position.Y - gravityWell.Position.Y;

            Vector2 force = new Vector2();
            force.X = - (deltaX * (mass / (distance * distance)));              // <-------------------- Gotta flip the X for some reason.
            force.Y =   (deltaY * (mass / (distance * distance)));

            //accelleration.X = Math.Abs(accelleration.X);
            //accelleration.Y = Math.Abs(accelleration.Y);

            rock.pushPull(force);

           
        }

        private Vector2 angleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }

        private float vectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.X, -vector.Y);
        }
        
        private void checkEnemyProjectileCollisions(Models.Gameworld gameWorld)
        {
            List<int> collidedProjectileIndexes = new List<int>();
            Models.Player playerObject = gameWorld.PlayerObject;

            for (int i = 0; i < gameWorld.EnemyList.Count; i++)
            {
                for (int j = 0; j < gameWorld.EnemyList[i].BulletList.Count; j++)
                {
                    if (playerObject.BoundingBox.Intersects(gameWorld.EnemyList[i].BulletList[j].BoundingBox))
                    {
                        if (perPixelCollisionDetection(playerObject, gameWorld.EnemyList[i].BulletList[j]))
                        {
                            collidedProjectileIndexes.Add(j);
                            playerProjectileCollisions.Add(gameWorld.EnemyList[i].BulletList[j]);
                        }
                    }
                }
                for (int k = 0; k < collidedProjectileIndexes.Count; k++)
                {
                    gameWorld.EnemyList[i].BulletList.RemoveAt(collidedProjectileIndexes[k]);
                }
                collidedProjectileIndexes.Clear();
            }
        }

        private void checkPlayerProjectileCollisions(Models.Gameworld gameWorld)
        {
            List<int> collidedProjectileIndexes = new List<int>();

            bool flaggBecauseFuckYouThatsWhy = false;

            for (int i = 0; i < gameWorld.getPlayer().BulletList.Count; i++)
            {
                flaggBecauseFuckYouThatsWhy = false;
                for (int j = 0; j < gameWorld.getRock().Count; j++)   
                {
                    if(gameWorld.getRock()[j].BoundingBox.Intersects(gameWorld.getPlayer().BulletList[i].BoundingBox))
                    {
                        if (perPixelCollisionDetection(gameWorld.getPlayer().BulletList[i],
                                                        gameWorld.getRock()[j]))
                        {
                            collidedProjectileIndexes.Add(i);
                            rockCollisionIndex.Add(j);
                            rockProjectileCollisions.Add(gameWorld.getPlayer().BulletList[i]);
                            rockProjectileCollisions.Add(gameWorld.getRock()[j]);
                            flaggBecauseFuckYouThatsWhy = true;
                            
                        }
                    }
                    if(flaggBecauseFuckYouThatsWhy) break;
                }
                if (!flaggBecauseFuckYouThatsWhy)
                {
                    for (int l = 0; l < gameWorld.EnemyList.Count; l++)
                    {
                        if (gameWorld.EnemyList[l].BoundingBox.Intersects(gameWorld.getPlayer().BulletList[i].BoundingBox))
                        {
                            if (perPixelCollisionDetection(gameWorld.getPlayer().BulletList[i],
                                                            gameWorld.EnemyList[l]))
                            {
                               
                                collidedProjectileIndexes.Add(i);
                                enemyCollisionIndex.Add(l);
                                enemyProjectileCollisions.Add(gameWorld.getPlayer().BulletList[i]);
                                enemyProjectileCollisions.Add(gameWorld.EnemyList[l]);
                                flaggBecauseFuckYouThatsWhy = true;
                            }
                        }
                        if (flaggBecauseFuckYouThatsWhy) break;
                    }
                }
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

        //Checks for collisions on the Player. 
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

            levelBoundCollision(gameWorld, prePosition);
        }

        private void levelBoundCollision(Models.Gameworld gameWorld, Vector2 prePosition)
        {
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

            //Vector2 velocity = playerObject.Velocity;

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
