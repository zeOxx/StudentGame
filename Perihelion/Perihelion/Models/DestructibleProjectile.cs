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
        bool isDestructable;

        public DestructibleProjectile(Texture2D texture, float x, float y, Vector2 velocity, int damage, bool isDestructable)
            : base(texture, x, y, velocity, damage)
        {
            setDestructable(isDestructable);
        }

        private void setDestructable(bool isDestructable)
        {
            this.isDestructable = isDestructable;
        }
    }
}
