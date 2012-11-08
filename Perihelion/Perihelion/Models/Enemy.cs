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
        protected Texture2D texture_bullet;
        protected Texture2D texture_rocket;
        //protected double turretRotationAngle = 0.0;

        // Cloak variables
        private bool cloak;
        private bool cloaked;
        private int cloakCountdown = 0;
        private static int cloakCooldown = 100;

        // Shoting variables
        private bool hasBullets;                // Does enemy have regular projectiles?
        private bool hasRockets;                // Does enemy have destructible projectiles?
        private bool shootingBullets;           // 
        private bool shootingRockets;           //
        private int timeBetweenBullets = 50;
        private int timeBetweenRockets = 300;
        private int bulletTimer = 0;
        private int rocketTimer = 0;
        private bool bulletMade = false;
        private bool rocketMade = false;
        private int bulletSpeed = 25;
        private int rocketSpeed = 15;
        private int bulletCounter = 0;
        private int rocketCounter = 0;

        // Create a lists with bullets/rockets in them
        List<Projectile> bullets;
        List<DestructibleProjectile> rockets;

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

        private bool Bullets
        {
            get { return this.hasBullets; }
            set { this.hasBullets = value; }
        }

        private bool ShootingBullets
        {
            get { return this.shootingBullets; }
            set { this.shootingBullets = value; }
        }

        public int BulletNumber
        {
            get { return bullets.Count(); }
        }

        public bool BulletMade
        {
            get { return this.bulletMade; }
            set { this.bulletMade = value; }
        }

        public int BulletTimeBetween
        {
            get { return this.timeBetweenBullets; }
            set { this.timeBetweenBullets = value; }
        }

        public List<Projectile> BulletList
        {
            get { return bullets; }
        }

        Texture2D BulletTexture
        {
            get { return this.texture_bullet; }
            set { this.texture_bullet = value; }
        }

        private bool Rockets
        {
            get { return this.hasRockets; }
            set { this.hasRockets = value; }
        }

        private bool ShootingRockets
        {
            get { return this.shootingRockets; }
            set { this.shootingRockets = value; }
        }

        public List<DestructibleProjectile> RocketList
        {
            get { return rockets; }
        }

        Texture2D RocketTexture
        {
            get { return this.texture_rocket; }
            set { this.texture_rocket = value; }
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
            //Console.Out.WriteLine(direction);
            if (Bullets)
            {
                updateBullets(gameTime, direction);
            }
            if (Rockets)
            {
                updateRockets(gameTime);
            }

            base.unitUpdate(direction);
        }

        private void updateBullets(GameTime gameTime, Vector2 bulletDirection)
        {
            if (ShootingBullets || BulletMade)
            {
                bulletTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (bulletTimer > timeBetweenBullets)
                {
                    // Reset bulletTimer
                    bulletMade = true;
                    bulletTimer = 0;

                    Projectile tempBullet = new Projectile(
                        texture_bullet,
                        this.position.X,
                        this.position.Y,
                        bulletDirection,
                        2000,   // ActiveTime in miliseconds (2 secs)
                        40,     // Damage amount Inge pulled out of his ass
                        bulletSpeed);

                    tempBullet.Identifier = bulletCounter++;

                    bullets.Add(tempBullet);
                }
                else
                {
                    if (bulletMade)
                        bulletMade = false;
                }
            }

            // Updates the bullets and removes them if they go over their activeTime
            for (int i = 0; i < bullets.Count(); i++)
            {
                bullets[i].update(gameTime);

                if (bullets[i].TotalActiveTime > bullets[i].ActiveTime)
                    bullets.RemoveAt(i);
            }
        }

        private void updateRockets(GameTime gameTime)
        {
            // TODO
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
