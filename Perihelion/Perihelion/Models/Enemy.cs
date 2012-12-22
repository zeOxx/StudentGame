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

        public Enemy(Texture2D texture, Texture2D texture_turret, Texture2D texture_bullet, float x, float y, Vector2 velocity, int health) //WIP
            : base(texture, x, y, velocity, health)
        {
            TurretTexture = texture_turret;
            BulletTexture = texture_bullet;
            Bullets = true;
            Rockets = false;
            Cloak = false;
            Cloaked = false;
            ShootingBullets = false;
            ShootingRockets = false;
            timeBetweenBullets = 1000;
            bulletSpeed = 5;

            // Temp
            
            MaxSpeed = 4;

            bullets = new List<Projectile>();
        }

        //public Enemy(Texture2D texture, Texture2D textureBullet, float posX, float posY, Vector2 velocity, int health)
        //    : base(texture, textureBullet, posX, posY, velocity, health)
        //{

        //}

        public Enemy(Texture2D texture, float x, float y, Vector2 velocity, int health, float damageMultiplier, float attackMultiplier, bool projectiles, bool destructibleProjectiles, bool cloak)
            : base(texture, x, y, velocity, health, damageMultiplier, attackMultiplier)
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
        public void update(Vector2 rightStick, GameTime gameTime, bool rocket)
        {
            //if (!leftStick.Equals(Vector2.Zero))
            //{
            //    leftStick = correctCourse(leftStick);
            //}

            base.unitUpdate(rightStick, gameTime);
        }

        //private Vector2 correctCourse(Vector2 direction)
        //{
        //    double maxAngle = 0.02d;
        //    //TODO!!!!!
        //    //float dot = Vector2.Dot(base.velocity, direction);

        //    //float angle = dot /
        //    //    (((float)Math.Sqrt(Math.Pow(base.velocity.X, 2) + Math.Pow(direction.X, 2)))
        //    //    * ((float)Math.Sqrt(Math.Pow(base.velocity.Y, 2) + Math.Pow(direction.Y, 2))));
        //    //angle = (float)(RadianToDegree(Math.Acos(angle)));
        //    ////Console.Out.WriteLine(angle);
        //    //if(angle > 5)
        //    //{
        //    //    Vector2 corrected = Vector2.Zero;
        //    //    return corrected;
        //    //}
        //    //else
        //    //{
        //    //    return direction;
        //    //}return (float) Math.Atan2(this.x * b.y - this.y * b.x, this.Dot(b));
        //    float angle = (float)Math.Atan2((double)
        //                    ((base.velocity.X * direction.Y)
        //                    - (base.velocity.Y * direction.X))
        //                    , Vector2.Dot(base.velocity, direction));
        //    Console.Out.WriteLine(angle);
        //    if (angle > maxAngle)
        //    {
        //        Console.Out.WriteLine("angle>maxangle");
        //        double cos = Math.Cos(maxAngle);
        //        double sin = Math.Sin(maxAngle);


        //        Vector2 newdir = Vector2.Zero;
        //        newdir.X = (float) (Velocity.X * cos - Velocity.Y * sin);
        //        newdir.Y = (float) (Velocity.X * sin + Velocity.X * cos);

        //        return newdir;
        //        //if (true){
                    
        //        //}
        //        //else
        //        //{
                    
        //        //}
        //    }
        //    else
        //    {
        //        return direction;
        //    }

        //}

        private void updateCloakcountdown(int i)
        {
            this.cloakCountdown -= i;
        }

        /************************************************************************/
        /*  Draw functions for Enemy attributes                                 */
        /************************************************************************/
        public override void Draw(SpriteBatch spriteBatch, bool debug)
        {
            if (Cloaked)
            {
                base.Draw(spriteBatch, 0.1f);
            }
            else
            {
                base.Draw(spriteBatch, debug);
            }
            
            foreach (Models.Projectile projectiles in bullets)
            {
                projectiles.Draw(spriteBatch, debug);
            }
        }
    }
}
