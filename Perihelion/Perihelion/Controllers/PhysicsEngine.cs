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
            projectileCollisionsIndex = new List<int>();
            rockCollisionIndex = new List<int>();
            enemyCollisionIndex = new List<int>();
            rockProjectileCollisions = new List<Models.GameObject>();
            enemyProjectileCollisions = new List<Models.GameObject>();
            playerProjectileCollisions = new List<Models.GameObject>();
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

            bounceThoseRocks(gameWorld.getRock());
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
            for (int i = 0; i < rocks.Count; i++)
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
            float distance = ((float)Math.Sqrt(((rock.Position.X - gravityWell.Position.X) *
                                                (rock.Position.X - gravityWell.Position.X))
                                                           +
                                               ((rock.Position.Y - gravityWell.Position.Y) *
                                                (rock.Position.Y - gravityWell.Position.Y))
                                       ));

            const int mass = 15;

            float deltaX = rock.Position.X - gravityWell.Position.X;
            float deltaY = rock.Position.Y - gravityWell.Position.Y;

            Vector2 force = new Vector2();
            force.X = -(deltaX * (mass / (distance * distance)));              // <-------------------- Gotta flip the X for some reason.
            force.Y = (deltaY * (mass / (distance * distance)));

            rock.pushPull(force);
        }



        private void bounceThoseRocks(List<Models.Collidable> rocks)
        {
            for (int i = 0; i < rocks.Count; i++)
            {
                for (int j = 0; j < rocks.Count; j++)
                {
                    if (!(j == i))
                    {
                        if (rocks[i].BoundingBox.Intersects(rocks[j].BoundingBox))
                        {
                            if (perPixelCollisionDetection(rocks[j], rocks[i]))
                            {
                                collisionUsingAllKindaCrazyStuff(rocks[i], rocks[j]);
                            }
                        }
                    }
                }
            }
        }



        /*
         *  WARNING! MASSIVE FUNCTION BELOW!!
         *  This proved to be a bit too complex....
         *  
         * It is supposed to provide collition resolution with rotation, angular innertia and all kinda funky stuff. 
         * Sorry to say, it just bugs out. 
         * 
         */
        public void collisionUsingAllKindaCrazyStuff(Models.GameObject objectA, Models.GameObject objectB)
        {
            double massObjectA = 10;
            double massObjectB = 10;

            //Elastic
            double e = 1;

            Vector2 initialAngularVelocityOfObjectA = objectA.AngularVelocity;
            Vector2 initialAngularVelocityOfObjectB = objectB.AngularVelocity;

            Vector2 initialVelocityObjectA = objectA.Velocity * objectA.Speed;
            Vector2 initialVelocityObjectB = objectB.Velocity * objectA.Speed;

            Vector2 collisionPointRelativeToBodyB = new Vector2(objectA.Position.X - objectB.Position.X, objectA.Position.Y - objectB.Position.Y);
            Vector2 collisionPointRelativeToBodyA = new Vector2(objectB.Position.X - objectA.Position.X, objectB.Position.Y - objectA.Position.Y);

            double lengthBetweenObjects = Math.Sqrt((collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyB.X) + (collisionPointRelativeToBodyA.Y * collisionPointRelativeToBodyA.Y));

            double innertiaBodyB = massObjectB * ((objectB.Position.X * objectB.Position.X) + (objectB.Position.Y * objectB.Position.Y));
            double innertiaBodyA = massObjectA * ((objectA.Position.X * objectA.Position.X) + (objectA.Position.Y * objectA.Position.Y));

            double k = (1 / (massObjectA * massObjectA) + (2 / (massObjectA * massObjectB))) + (1 / (massObjectB * massObjectB)) - (collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyA.X) / (massObjectA * innertiaBodyA) - (collisionPointRelativeToBodyB.X * collisionPointRelativeToBodyB.X) / (massObjectA * innertiaBodyB) - (collisionPointRelativeToBodyA.Y * collisionPointRelativeToBodyA.Y) / (massObjectA * innertiaBodyA)
                         - (collisionPointRelativeToBodyA.Y * collisionPointRelativeToBodyA.Y) / (massObjectB * innertiaBodyA) - (collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyA.X) / (massObjectB * innertiaBodyA) - (collisionPointRelativeToBodyB.X * collisionPointRelativeToBodyB.X) / (massObjectB * innertiaBodyB) - (collisionPointRelativeToBodyB.Y * collisionPointRelativeToBodyB.Y) / (massObjectA * innertiaBodyB)
                         - (collisionPointRelativeToBodyB.Y * collisionPointRelativeToBodyB.Y) / (massObjectB * innertiaBodyB) + (collisionPointRelativeToBodyA.Y * collisionPointRelativeToBodyA.Y * collisionPointRelativeToBodyB.X * collisionPointRelativeToBodyB.X) / (innertiaBodyA * innertiaBodyB) + (collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyB.Y * collisionPointRelativeToBodyB.Y) / (innertiaBodyA * innertiaBodyB) - (2 * collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyA.Y * collisionPointRelativeToBodyB.X * collisionPointRelativeToBodyB.Y) / (innertiaBodyA * innertiaBodyB);

            double Jx =   (e + 1) /  k * (initialVelocityObjectA.X - initialVelocityObjectB.X) * (1 / massObjectA - collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyA.X / innertiaBodyA + 1 / massObjectB - collisionPointRelativeToBodyB.X * collisionPointRelativeToBodyB.X / innertiaBodyB)
                        - (e + 1) /  k * (initialVelocityObjectA.Y - initialVelocityObjectB.Y) * (collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyA.Y / innertiaBodyA + collisionPointRelativeToBodyB.X * collisionPointRelativeToBodyB.Y / innertiaBodyB);

            double Jy = - (e + 1) /  k * (initialVelocityObjectA.X - initialVelocityObjectB.X) * (collisionPointRelativeToBodyA.X * collisionPointRelativeToBodyA.Y / innertiaBodyA + collisionPointRelativeToBodyB.X * collisionPointRelativeToBodyB.Y / innertiaBodyB)
                        + (e + 1) /  k * (initialVelocityObjectA.Y - initialVelocityObjectB.Y) * (1 / massObjectA - collisionPointRelativeToBodyA.Y * collisionPointRelativeToBodyA.Y / innertiaBodyA + 1 / massObjectB - collisionPointRelativeToBodyB.Y * collisionPointRelativeToBodyB.Y / innertiaBodyB);

            Vector2 newVelocityA;
            newVelocityA.X =  (float) (initialVelocityObjectA.X - (Jx / massObjectA));
            newVelocityA.Y =  (float) (initialVelocityObjectA.Y - (Jy / massObjectA));
            objectA.Velocity = newVelocityA / objectA.Speed;

            Vector2 newVelocityB;
            newVelocityB.X = (float)(initialVelocityObjectB.X - (Jx / massObjectB));
            newVelocityB.Y = (float)(initialVelocityObjectB.Y - (Jy / massObjectB));
            objectB.Velocity = newVelocityB / objectB.Speed;

            objectA.AngularVelocity = new Vector2 ( (float) (initialAngularVelocityOfObjectA.X - (Jx * collisionPointRelativeToBodyA.Y - Jy * collisionPointRelativeToBodyA.X) / innertiaBodyA),
                                                    (float) (initialAngularVelocityOfObjectA.Y - (Jx * collisionPointRelativeToBodyA.Y - Jy * collisionPointRelativeToBodyA.X) / innertiaBodyA));
            objectB.AngularVelocity = new Vector2 ( (float) (initialAngularVelocityOfObjectB.X - (Jx * collisionPointRelativeToBodyB.Y - Jy * collisionPointRelativeToBodyB.X) / innertiaBodyB),
                                                    (float) (initialAngularVelocityOfObjectB.Y - (Jx * collisionPointRelativeToBodyB.Y - Jy * collisionPointRelativeToBodyB.X) / innertiaBodyB));

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

                if (collidedProjectileIndexes.Count > 0)
                {
                    collidedProjectileIndexes.Sort();

                    //for (int i = 0; i < collidedProjectileIndexes.Count; i++)
                    for (int k = collidedProjectileIndexes.Count - 1; k >= 0; k--)
                    {
                        gameWorld.EnemyList[i].BulletList.RemoveAt(collidedProjectileIndexes[k]);
                    }
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
                    if (gameWorld.getRock()[j].BoundingBox.Intersects(gameWorld.getPlayer().BulletList[i].BoundingBox))
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
                    if (flaggBecauseFuckYouThatsWhy) break;
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
                collidedProjectileIndexes.Sort();

                //for (int i = 0; i < collidedProjectileIndexes.Count; i++)
                for (int i = collidedProjectileIndexes.Count - 1; i >= 0; i--)
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
                        //gameWorld.getPlayer().Velocity = (Vector2.Zero);

                        knockback(gameWorld.PlayerObject, rock);
                        break;
                    }
                }
            }

            levelBoundCollision(gameWorld, prePosition);
        }


        // Not finished.
        private void knockback(Models.GameObject knockBackObject, Models.GameObject collidingObject)
        {
            collidingObject.Velocity += (knockBackObject.Velocity * 0.7f);

            knockBackObject.Velocity = ((Vector2.Normalize(-knockBackObject.Velocity) * 1.2f));
        }

        private void levelBoundCollision(Models.Gameworld gameWorld, Vector2 prePosition)
        {
            if (!gameWorld.getPlayer().BoundingBox.Intersects(gameWorld.LevelBounds))
            {
                gameWorld.getPlayer().Position = new Vector2(prePosition.X, prePosition.Y);
                gameWorld.getPlayer().Velocity = Vector2.Zero;
            }
        }


        /*
         * Checks for per pixel collision between two objects.
         * Typically called after returning true on intersecting boundingboxes. 
         */
        private bool perPixelCollisionDetection(Models.GameObject playerObject, Models.GameObject collidingObject)
        {
            int top = Math.Max(playerObject.BoundingBox.Top, collidingObject.BoundingBox.Top);
            int bottom = Math.Min(playerObject.BoundingBox.Bottom, collidingObject.BoundingBox.Bottom);
            int left = Math.Max(playerObject.BoundingBox.Left, collidingObject.BoundingBox.Left);
            int right = Math.Min(playerObject.BoundingBox.Right, collidingObject.BoundingBox.Right);

            Vector2 position = playerObject.Position;

            Texture2D playerTexture = playerObject.Texture;
            Texture2D collidableTexture = collidingObject.Texture;

            Color[] playerTextureData = new Color    [playerTexture.Width     * playerTexture.Height];
            Color[] collidableTextureData = new Color[collidableTexture.Width * collidableTexture.Height];

            playerTexture.GetData(playerTextureData);
            collidableTexture.GetData(collidableTextureData);

            for (int y = top; y < bottom; y++)
            {
                int rowOffsetPlayer  = (y - playerObject.BoundingBox.Top)    * playerObject.BoundingBox.Width;
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
