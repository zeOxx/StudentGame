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

            Vector2 direction = enemyvector - playervector;
            
            direction = direction / direction.Length();
            direction.X = direction.X * -1;

            return direction;
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
