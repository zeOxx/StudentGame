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
        // Texture variables
        //protected Texture2D texture_turret;
        //protected Texture2D texture_bullet;
        //protected double turretRotationAngle = 0.0;

        // Cloak variables
        private bool cloak;
        private bool cloaked;
        private int cloakCountdown = 0;
        private static int cloakCooldown = 100;

        // Shoting variables
        private bool hasProjectiles;                // Does enemy have regular projectiles?
        private bool hasDestructibleProjectiles;    // Does enemy have destructible projectiles?
        private bool isShooting;
        private int timeBetweenShots = 50;
        private int shotTimer = 0;
        private bool bulletMade = false;
        private int bulletSpeed = 15;
        private int bulletCounter = 0;

        /************************************************************************/
        /*  Constructors for Enemy object                                       */
        /************************************************************************/
        public Enemy()
            : base()
        {
            Projectiles = true;
            DestructibleProjectiles = true;
            Cloak = false;
            Cloaked = false;
        }

        public Enemy(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth) //WIP
            : base(texture, x, y, velocity, currentHealth, maxHealth)
        {
            Projectiles = true;
            DestructibleProjectiles = true;
            Cloak = false;
            Cloaked = false;
        }

        public Enemy(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier, bool projectiles, bool destructibleProjectiles, bool cloak)
            : base(texture, x, y, velocity, currentHealth, maxHealth, damageMultiplier, attackMultiplier)
        {
            Projectiles = projectiles;
            DestructibleProjectiles = destructibleProjectiles;
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
        void setCloaked(bool cloaked)
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

        bool Projectiles
        {
            get { return this.hasProjectiles; }
            set { this.hasProjectiles = value; }
        }

        bool DestructibleProjectiles
        {
            get { return this.hasDestructibleProjectiles; }
            set { this.hasDestructibleProjectiles = value; }
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
        private void updateCloakcountdown(int i)
        {
            this.cloakCountdown -= i;
        }
    }
}
