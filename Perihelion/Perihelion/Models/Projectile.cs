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
        private int damage;
        private int activeTime;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Projectile(Texture2D texture, float x, float y, Vector2 velocity, int damage, int activeTime) : base(texture, x, y, velocity)
        {
            Damage = damage;
            ActiveTime = activeTime;
        }

        /************************************************************************/
        /* Setters and getters                                                  */
        /************************************************************************/
        private int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        private int ActiveTime
        {
            get { return this.activeTime; }
            set { this.activeTime = value; }
        }
    }
}
