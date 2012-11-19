using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class DestructibleProjectile : Projectile
    {
        bool isDestructible;

        public DestructibleProjectile(Texture2D texture, float x, float y, Vector2 velocity,int activeTime, int damage, float speed, bool isDestructible)
            : base(texture, x, y, velocity, activeTime, damage, speed)
        {
            Destructible = isDestructible;
        }

        public bool Destructible
        {
            get { return this.isDestructible; }
            private set { this.isDestructible = value; }
        }
    }
}
