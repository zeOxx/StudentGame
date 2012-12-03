using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Perihelion.Controllers
{
    class UnitHandler
    {
        private Models.Gameworld gameworld;

        private static float enemyBufferDistance = 100;
        private static float enemyAggroDistance = 300;


        public UnitHandler()
        {

        }

        public Vector2 getEnemyStickVector(Models.Player player, Models.Enemy enemy)
        {
            Vector2 enemyvector = enemy.Position;
            Vector2 playervector = player.Position;
            double oldangle = player.RotationAngle;

            Vector2 direction = enemyvector - playervector;
            direction = direction / direction.Length();
            direction.X = direction.X * -1;

            return direction;
        }

        public Vector2 restrictEnemy(Models.Enemy enemy, Vector2 target)
        {
            float restrict = 0.01f;
            Vector2 vector;
            if (lessThanAlpha(enemy.Direction, target, restrict))
            {
                return target;
            }
            else
            {
                Console.Out.WriteLine(angle(enemy.Direction));
                float c = cross(enemy.Direction, target);

                if (c < 0) // turn right
                {
                    vector = rotate(enemy.Direction, -restrict);
                    Console.Out.Write("turning right: ");
                    Console.Out.WriteLine(vector);
                    return vector;
                }
                else //if (c >= 0) // turn left
                {
                    vector = rotate(enemy.Direction, restrict);
                    Console.Out.Write("turning left: ");
                    Console.Out.WriteLine(vector);
                    return vector;
                }
            }
        }
        
        public float angle(Vector2 vector)
        {
            return (float)(Math.Atan2(vector.X, vector.Y));
        }

        public float cross(Vector2 dir, Vector2 target)
        {
            float c = dir.X * target.Y - dir.Y * target.X;
            return c;
        }

        public bool lessThanAlpha(Vector2 a, Vector2 b, float alpha)
        {
            a.Normalize();
            b.Normalize();
            return Vector2.Dot(a, b) < Math.Cos(alpha);
        }

        public Vector2 rotate(Vector2 vector, float angle)
        {
            Vector2 result = Vector2.Zero;
            result.X = (float) (vector.X * Math.Cos(angle) - vector.Y * Math.Sin(angle));
            result.Y = (float) (vector.X * Math.Sin(angle) + vector.Y * Math.Cos(angle));
            return result;
        }

        public int getEnemyTarget(Models.Player player, Models.Enemy enemy)
        {
            Vector2 enemyvector = enemy.Position;
            Vector2 playervector = player.Position;

            Vector2 diff = enemyvector - playervector;
            if (enemyAggroDistance < diff.Length())
            {
                return 2;
            }
            else if (diff.Length() < enemyBufferDistance)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public void updateHandler(Models.Gameworld gameworld)
        {
            this.gameworld = gameworld;
        }
    }
}
