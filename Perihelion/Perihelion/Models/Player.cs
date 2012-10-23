using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Player : Unit
    {
        protected Texture2D texture_turret;
        protected Texture2D texture_bullet;
        protected double turretRotationAngle = 0.0;
        
        // Powers and stuff
        private float wellMultiplier;
        private int wellStatus;
        private int auxiliaryPower;

        // Shooting variables
        private bool isShooting;
        private int timeBetweenShots = 50;
        private int shotTimer = 0;
        private bool bulletMade = false;
        private int bulletSpeed = 15;

        // Create a list with bullets in it
        List<Projectile> bullets;

        /************************************************************************/
        /*  Constructors for Player object                                      */
        /************************************************************************/
        public Player()
            : base()
        {
            setWellMultiplier(1);
            setWellStatus(0);
            setAuxiliaryPower(100);
        }

        public Player(Texture2D texture_ship, Texture2D texture_turret, Texture2D texture_bullet, float x, float y, Vector2 velocity, int currentHealth, int maxHealth)
            : base(texture_ship, x, y, velocity, currentHealth, maxHealth)
        {
            setTurretTexture(texture_turret);
            setBulletTexture(texture_bullet);
            setWellMultiplier(1);
            setWellStatus(0);
            setAuxiliaryPower(100);

            // Temp
            Speed = 5;
            setMaxSpeed(5);

            IsShooting = false;

            bullets = new List<Projectile>();
        }

        public Player(Texture2D texture, Texture2D texture_turret, Texture2D texture_bullet, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier, float wellMultiplier, int wellStatus, int auxiliaryPower)
            : base(texture, x, y, velocity, currentHealth, maxHealth, damageMultiplier, attackMultiplier)
        {
            setTurretTexture(texture_turret);
            setWellMultiplier(wellMultiplier);
            setWellStatus(wellStatus);
            setAuxiliaryPower(auxiliaryPower);
        }
        /************************************************************************/
        /*  Set functions for Player attributes                                 */
        /************************************************************************/
        void setTurretTexture(Texture2D texture_turret)
        {
            this.texture_turret = texture_turret;
        }

        void setBulletTexture(Texture2D texture_bullet)
        {
            this.texture_bullet = texture_bullet;
        }
        
        void setWellMultiplier(float wellModifier)
        {
            this.wellMultiplier = wellModifier;
        }

        void setWellStatus(int wellStatus)
        {
            this.wellStatus = wellStatus;
        }

        void setAuxiliaryPower(int auxiliaryPower)
        {
            this.auxiliaryPower = auxiliaryPower;
        }

        /************************************************************************/
        /*  Get functions for Player attributes                                 */
        /************************************************************************/
        float getWellMultiplier()
        {
            return this.wellMultiplier;
        }

        int getWellStatus()
        {
            return this.wellStatus;
        }

        int getAuxiliaryPower()
        {
            return this.auxiliaryPower;
        }

        public int getNumberOfBullets()
        {
            return bullets.Count();
        }

        public bool IsShooting
        {
            get { return this.isShooting; }
            set { this.isShooting = value; }
        }

        public bool BulletMade
        {
            get { return this.bulletMade; }
            set { this.bulletMade = value; }
        }

        /************************************************************************/
        /*  Update functions for Player attributes                              */
        /************************************************************************/
        public void update(Vector2 velocity, Vector2 rightStick, GameTime gameTime)
        {
            updateTurret(rightStick);

            if (isShooting || bulletMade)
            {
                shotTimer += gameTime.ElapsedGameTime.Milliseconds;

                if (shotTimer > timeBetweenShots)
                {
                    // Reset shotTimer
                    bulletMade = true;
                    shotTimer = 0;

                    Projectile tempBullet = new Projectile(
                        texture_bullet,
                        this.position.X,
                        this.position.Y,
                        rightStick,
                        2000,   // ActiveTime in miliseconds (2 secs)
                        40,     // Damagenumber i pulled out of my ass
                        bulletSpeed);

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

            base.update(velocity);
        }

        public void updateWellMultiplier(float i)
        {
            this.wellMultiplier += i;
        }

        public void updateAuxiliaryPower(int i)
        {
            this.auxiliaryPower += i;
        }

        public void updateTurret(Vector2 rightStick)
        {
            if ((rightStick.X < 0.0f || rightStick.Y < 0.0f) || (rightStick.X > 0.0f || rightStick.Y > 0.0f))
            {
                turretRotationAngle = Math.Atan2((double)rightStick.X, (double)rightStick.Y);
                IsShooting = true;
            }
            if (rightStick.X == 0.0f && rightStick.Y == 0.0f)
                IsShooting = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Draw(texture_turret,
                position,
                null,
                Color.White,
                (float)turretRotationAngle,
                origin,
                1.0f,
                SpriteEffects.None,
                0f);

            foreach (Models.Projectile projectiles in bullets)
            {
                projectiles.Draw(spriteBatch);
            }
        }
    }
}
