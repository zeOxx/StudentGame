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

        public UnitHandler()
        {

        }

        public Vector2 getEnemyStickVector(Models.Player player, Models.Enemy enemy)
        {
            Vector2 enemyvector = enemy.getPosition();
            Vector2 playervector = player.getPosition();

            Vector2 direction = enemyvector - playervector;
            direction = direction / direction.Length();
            direction.X = direction.X * -1;
            return direction;
        }

        public bool getEnemyTarget(Models.Player player, Models.Enemy enemy)
        {
            Vector2 enemyvector = enemy.getVelocity();
            Vector2 playervector = player.getPosition();

            return false;
        }

        public void updateHandler(Models.Gameworld gameworld)
        {
            this.gameworld = gameworld;
        }
    }
}
