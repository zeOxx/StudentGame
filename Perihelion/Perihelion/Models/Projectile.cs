using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Projectile : GameObject
    {
        int damage;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Projectile(Texture2D texture, float x, float y, Vector2 velocity, int damage) : base(texture, x, y, velocity)
        {
            setDamage(damage);
        }

        /************************************************************************/
        /* Setters                                                              */
        /************************************************************************/
        private void setDamage(int damage)
        {
            this.damage = damage;
        }

        /************************************************************************/
        /* Getters                                                              */
        /************************************************************************/
        public int getDamage()
        {
            return damage;
        }
    }
}
