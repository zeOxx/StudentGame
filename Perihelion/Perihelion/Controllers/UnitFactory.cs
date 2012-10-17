using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Controllers
{
    class UnitFactory
    {
        private enum units { enemy, player };
        public UnitFactory()
        {
            
        }

        Models.GameObject createUnit(units unitType){
            switch (unitType)
            {
                case units.enemy:
                    return new Models.Enemy();
                case units.player:
                    return new Models.Player();
                default:
                    return null;
            }
        }

        /*
        Models.Player createPlayer(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth)
        {
            Models.Player player = new Models.Player(texture, x, y, velocity, currentHealth, maxHealth);
            return player;
        }

        Models.Player createPlayer(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier, float wellMultiplier, int wellStatus, int auxiliaryPower)
        {
            Models.Player player = new Models.Player(texture, x, y, velocity, currentHealth, maxHealth, damageMultiplier, attackMultiplier, wellMultiplier, wellStatus, auxiliaryPower);
            return player;
        }

        Models.Enemy createEnemy(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth)
        {
            Models.Enemy enemy = new Models.Enemy(texture, x, y, velocity, currentHealth, maxHealth);
            return enemy;
        }

        Models.Enemy createEnemy(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier, bool projectiles, bool destructibleProjectiles, bool cloak)
        {
            Models.Enemy enemy = new Models.Enemy(texture, x, y, velocity, currentHealth, maxHealth, damageMultiplier, attackMultiplier, projectiles, destructibleProjectiles, cloak);
            return enemy;
        }*/
    }
}
