using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Enemy : Unit
    {
        //protected double turretRotationAngle = 0.0;

        // Cloak variables
        private bool cloak;
        private bool cloaked;
        private int cloakCountdown = 0;
        private static int cloakCooldown = 100;


        /************************************************************************/
        /*  Constructors for Enemy object                                       */
        /************************************************************************/
        public Enemy()
            : base()
        {
            Bullets = true;
            Rockets = false;
            Cloak = false;
            Cloaked = false;
        }

        public Enemy(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth) //WIP
            : base(texture, x, y, velocity, currentHealth, maxHealth)
        {
            Bullets = true;
            Rockets = false;
            Cloak = false;
            Cloaked = false;
            ShootingBullets = false;
            ShootingRockets = false;

            // Temp
            Speed = 0;
            MaxSpeed = 4;

            bullets = new List<Projectile>();
            rockets = new List<DestructibleProjectile>();
        }

        public Enemy(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier, bool projectiles, bool destructibleProjectiles, bool cloak)
            : base(texture, x, y, velocity, currentHealth, maxHealth, damageMultiplier, attackMultiplier)
        {
            Bullets = projectiles;
            Rockets = destructibleProjectiles;
            Cloak = cloak;
            if (Cloak)
            {
                Cloaked = true;
            }
            else
            {
                Cloaked = false;
            }
        }
        /************************************************************************/
        /*  Get/Set functions for Enemy attributes                              */
        /************************************************************************/
        private void setCloaked(bool cloaked)
        {
            if (!Cloaked && Cloak)
            {
                this.cloakCountdown = cloakCooldown;
                Cloaked = cloaked;
            }
            else if (cloakCountdown > 0 && Cloaked)
            {
                Cloaked = cloaked;
            }
        }

        

        public bool Cloak
        {
            get { return this.cloak; }
            set { this.cloak = value; }
        }

        public bool Cloaked
        {
            get { return this.cloaked; }
            set { this.cloaked = value; }
        }

        private int CloakCountdown
        {
            get { return this.cloakCountdown; }
            set { this.cloakCountdown = value; }
        }

        /************************************************************************/
        /*  Update functions for Enemy attributes                               */
        /************************************************************************/
        public void update(Vector2 direction, GameTime gameTime, bool rocket)
        {
            if (base.Bullets)
            {
                base.updateBullets(gameTime, direction);
            }
            if (base.Rockets)
            {
                base.updateRockets(gameTime);
            }

            //direction = correctCourse(direction);

            base.unitUpdate(direction);
        }

        private Vector2 correctCourse(Vector2 direction)
        {
            Vector2 corrected = Vector2.Zero;

            return corrected;
        }

        private void updateCloakcountdown(int i)
        {
            this.cloakCountdown -= i;
        }

        /************************************************************************/
        /*  Draw functions for Enemy attributes                                 */
        /************************************************************************/
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            foreach (Models.Projectile projectiles in bullets)
            {
                projectiles.Draw(spriteBatch);
            }
            foreach (Models.DestructibleProjectile projectiles in rockets)
            {
                projectiles.Draw(spriteBatch);
            }
        }
    }
}
