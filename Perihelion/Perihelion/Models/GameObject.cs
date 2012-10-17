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
        private double rotationAngle = 0.0;
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

        void setOrigin(Texture2D texture)
        {
            // Sets the origin to the center of the object
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public void setOriginZero()
        {
            // Sets the origin to 0,0
            this.origin = new Vector2(0, 0);
        }

        protected void setSpeed(float speed)
        {
            this.speed = speed;
        }

        protected void setMaxSpeed(float maxSpeed)
        {
            this.maxSpeed = maxSpeed;
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

        public float getSpeed()
        {
            return this.speed;
        }

        public float getMaxSpeed()
        {
            return this.maxSpeed;
        }
        /************************************************************************/
        /*                                                                      */
        /************************************************************************/
        public virtual void update (Vector2 velocity)
        {
            updatePosition();
            updateVelocity(velocity);
            updateAngle(velocity);
        }

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
            // Only updates the sprite if there is velocity.
            if (velocity.X != 0.0f && velocity.Y != 0.0f)
            {
                System.Diagnostics.Debug.WriteLine("Boooi");
                rotationAngle = Math.Atan2((double)velocity.X, (double)velocity.Y);
            }
        }

               

        public void updateSpeed(float speedUpdate)
        {
            if (speed < maxSpeed && speed > 0)
                speed += speedUpdate;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(texture, position, Color.White);
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