using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Unit : GameObject
    {
        // Texture variables
        protected Texture2D texture_turret;
        protected Texture2D texture_bullet;
        protected Texture2D texture_rocket;
        
        private float damageMultiplier;
        private float attackMultiplier;
        private float accelerationMultiplier = 0.02f;

        // Shoting variables
        protected bool hasBullets;                // Does enemy have regular projectiles?
        protected bool hasRockets;                // Does enemy have destructible projectiles?
        protected bool shootingBullets;           // 
        protected bool shootingRockets;           //
        protected int timeBetweenBullets = 50;
        protected int timeBetweenRockets = 300;
        protected int bulletTimer = 0;
        protected int rocketTimer = 0;
        protected bool bulletMade = false;
        protected bool rocketMade = false;
        protected int bulletSpeed = 25;
        protected int rocketSpeed = 15;
        protected int bulletCounter = 0;
        protected int rocketCounter = 0;
        protected double turretRotationAngle = 0.0;

        // Create a lists with bullets/rockets in them
        protected List<Projectile> bullets;
        protected List<DestructibleProjectile> rockets;

        /************************************************************************/
        /*  Constructors for Unit object                                        */
        /************************************************************************/
        public Unit()
            : base()
        {
            DamageMultiplier = 1;
            AttackMultiplier = 1;
        }

        public Unit(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth)
            : base(texture, x, y, velocity, currentHealth, maxHealth)
        {
            DamageMultiplier = 1;
            AttackMultiplier = 1;
        }

        public Unit(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier)
            : base(texture, x, y, velocity, currentHealth, maxHealth)
        {
            DamageMultiplier = damageMultiplier;
            AttackMultiplier = attackMultiplier;
        }

        

        /************************************************************************/
        /*  Getter/setter functions for Unit attributes                         */
        /************************************************************************/
        protected float DamageMultiplier
        {
            get { return this.damageMultiplier; }
            set { this.damageMultiplier = value; }
        }

        protected float AttackMultiplier
        {
            get { return this.attackMultiplier; }
            set { this.attackMultiplier = value; }
        }
        
        protected bool Bullets
        {
            get { return this.hasBullets; }
            set { this.hasBullets = value; }
        }

        protected bool ShootingBullets
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

        protected Texture2D BulletTexture
        {
            get { return this.texture_bullet; }
            set { this.texture_bullet = value; }
        }

        protected bool Rockets
        {
            get { return this.hasRockets; }
            set { this.hasRockets = value; }
        }

        protected bool ShootingRockets
        {
            get { return this.shootingRockets; }
            set { this.shootingRockets = value; }
        }

        public List<DestructibleProjectile> RocketList
        {
            get { return rockets; }
        }

        protected Texture2D RocketTexture
        {
            get { return this.texture_rocket; }
            set { this.texture_rocket = value; }
        }

        protected Texture2D TurretTexture
        {
            get { return this.texture_turret; }
            set { this.texture_turret = value; }
        }

        /************************************************************************/
        /*  Update functions for Unit attributes                                */
        /************************************************************************/
        public void unitUpdate (Vector2 leftStick, Vector2 rightStick, GameTime gameTime)
        {
            updateTurret(rightStick);

            if (Bullets)
            {
                updateBullets(gameTime, rightStick);
            }
            if (Rockets)
            {
                updateRockets(gameTime);
            }
            base.update(scaleVelocity(leftStick));
        }

        void updateCurrentHealth(int i)
        {
            CurrentHealth += i;
        }

        void updateMaxHealth(int i)
        {
            MaxHealth += i;
        }

        private Vector2 scaleVelocity (Vector2 velocity)
        {
            float oldlength = this.velocity.Length();
            float newlength = velocity.Length();

            if (newlength > (oldlength + accelerationMultiplier))
            {
                if ((oldlength + accelerationMultiplier) <= 1)
                {
                    speed = maxSpeed * (oldlength + accelerationMultiplier);
                    velocity = velocity / velocity.Length() * (oldlength + accelerationMultiplier);
                }
                else
                {
                    speed = maxSpeed;
                }
                return velocity;
            }
            else if (newlength < (oldlength - accelerationMultiplier))
            {
                speed = maxSpeed * (oldlength - accelerationMultiplier);
                if (velocity.Length() != 0)
                {
                    velocity = velocity / velocity.Length() * (oldlength - accelerationMultiplier);
                }
                else
                {
                    velocity = this.velocity/ oldlength * (oldlength - accelerationMultiplier);
                }
                return velocity;
            }
            else
            {
                speed = newlength * maxSpeed;
                return velocity;
            }
        }

        protected void updateBullets(GameTime gameTime, Vector2 bulletDirection)
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

        protected void updateRockets(GameTime gameTime)
        {
            // TODO
        }

        public void updateTurret(Vector2 rightStick)
        {
            if ((rightStick.X < 0.0f || rightStick.Y < 0.0f) || (rightStick.X > 0.0f || rightStick.Y > 0.0f))
            {
                turretRotationAngle = Math.Atan2((double)rightStick.X, (double)rightStick.Y);
                shootingBullets = true;
            }
            if (rightStick.X == 0.0f && rightStick.Y == 0.0f)
                shootingBullets = false;
        }

        /************************************************************************/
        /*  Constructor functions for Unit attributes                         */
        /************************************************************************/
        protected void constructUnit(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier)
        {
            base.constructGameObject(texture, x, y, velocity);
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
            DamageMultiplier = damageMultiplier;
            AttackMultiplier = attackMultiplier;
        }
    }
}
