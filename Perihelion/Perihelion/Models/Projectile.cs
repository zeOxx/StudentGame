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
        private int activeTime;
        private int totalActiveTime;
        private int damage;
        

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Projectile(Texture2D texture, float x, float y, Vector2 velocity, int activeTime, int damage, float speed) : base(texture, x, y, velocity, 0)
        {
            ActiveTime = activeTime;
            Damage = damage;



            //double stigningsTall = velocity.Y / velocity.X;
            //double deltaStigning = 1 - stigningsTall;
            //velocity.Y = (float)deltaStigning * velocity.Y;
            //velocity.X = (float)deltaStigning * velocity.X; 

            Speed = speed;

            setBulletDirection(velocity);

            TotalActiveTime = 0;

            setOrigin(new Vector2((getTexture().Width/2), getTexture().Height));
        }
         
        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public int ActiveTime
        {
            get { return this.activeTime; }
            set { this.activeTime = value; }
        }
        
        public int Damage
        {
            get { return this.damage; }
            set { this.damage = value; }
        }

        public int TotalActiveTime
        {
            get { return this.totalActiveTime; }
            set { this.totalActiveTime = value; }
        }

        

        /************************************************************************/
        /* Other methods                                                        */
        /************************************************************************/
        public void update(GameTime gameTime)
        {
            double degrees = (Math.Atan2((double)velocity.X, (double)velocity.Y));
            velocity.X = (float)Math.Sin(degrees);
            velocity.Y = (float)Math.Cos(degrees);

            base.update(velocity);

            this.totalActiveTime += gameTime.ElapsedGameTime.Milliseconds;
        }

        public void setBulletDirection(Vector2 rightStick)
        {
            rotationAngle = Math.Atan2((double)rightStick.X, (double)rightStick.Y);
        }
    }
}