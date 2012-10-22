using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class GameObject
    {
        protected Texture2D texture;
        protected Vector2 origin;
        protected Vector2 position;
        protected Vector2 velocity;
        protected double rotationAngle = 0.0;
        protected float maxSpeed = 0;
        protected float speed = 0;
        
        /************************************************************************/
        /*                                                                      */
        /************************************************************************/
        public GameObject()
        {
            setPosition(0, 0);
            setVelocity(new Vector2(0, 0));
        }
        
        public GameObject (Texture2D texture, float x, float y, Vector2 velocity)
        {
            setTexture(texture);
            setPosition(x, y);
            setVelocity(velocity);
            setOrigin(texture);
        }

        /************************************************************************/
        /*  Creates a boundingbox based on the texture                          */
        /************************************************************************/
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    texture.Width,
                    texture.Height);
            }
        }

        /************************************************************************/
        /*                                                                      */
        /************************************************************************/
        void setTexture (Texture2D texture)
        {
            this.texture = texture;
        }

        public void setPosition (float x, float y)
        {
            this.position = new Vector2(x, y);
        }

        void setVelocity (Vector2 velocity)
        {
            this.velocity = velocity;
        }
        
        // Three different setOrigin methods.
        //  1.setOrigin(Texture2D texture): Sets the origin at the center of the texture
        //  2.setOrigin(Vector2 newOrigin): Sets the origin to whatever the newOrigin vector is
        //  3.setOriginZero(): Sets the origin to 0,0.
        //
        //  Number 1 is the default method default
        public void setOrigin(Texture2D texture)
        {
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void setOrigin(Vector2 newOrigin)
        {
            this.origin = newOrigin;
        }

        public void setOriginZero()
        {
            this.origin = new Vector2(0, 0);
        }
        // ***** END OF ORIGIN METHODS ***** //

        protected void setMaxSpeed(float maxSpeed)
        {
            this.maxSpeed = maxSpeed;
        }

        protected float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        /************************************************************************/
        /*                                                                      */
        /************************************************************************/
        public Vector2 getPosition ()
        {
            return position;
        }

        public Vector2 getVelocity()
        {
            return this.velocity;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public float getMaxSpeed()
        {
            return this.maxSpeed;
        }
        /************************************************************************/
        /* Other methods                                                        */
        /************************************************************************/
        public virtual void update (Vector2 velocity)
        {
            updatePosition();

            updateVelocity(velocity);
            updateAngle(velocity);
        }

        // TEMP KEYBOARD UPDATING
        public void updatePosition(float deltaX, float deltaY)
        {
            position.X = position.X + deltaX;
            position.Y = position.Y + deltaY;
        }

        public void updatePosition()
        {
            position.X = position.X + (velocity.X * speed);
            position.Y = position.Y - (velocity.Y * speed);
        }

        public void updateVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
        }

        public void updateAngle(Vector2 velocity)
        {

            if ((velocity.X < 0.0f || velocity.Y < 0.0f) || (velocity.X > 0.0f || velocity.Y > 0.0f))
                rotationAngle = Math.Atan2((double)velocity.X, (double)velocity.Y);
        }

               

        public void updateSpeed(float speedUpdate)
        {
            if (speed < maxSpeed && speed > 0)
                speed += speedUpdate;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, (float)rotationAngle,
                    origin, 1.0f, SpriteEffects.None, 0f);
        }


        protected void constructGameObject(Texture2D texture, float x, float y, Vector2 velocity)
        {
            setTexture(texture);
            setPosition(x, y);
            setVelocity(velocity);
        }
    }
}