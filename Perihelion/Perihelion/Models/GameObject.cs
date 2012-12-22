using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace Perihelion.Models
{
    class GameObject
    {
        protected Texture2D texture;
        protected Texture2D rectangle = Perihelion.ContentHolder.textureRectangle;
        //private ArrayList textureData;
        protected Vector2 origin;
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 direction = new Vector2(-1, 0);
        protected Vector2 angularVelocity;
        protected double rotationAngle = 0.0;
        protected float maxSpeed = 0;
        protected float speed = 0;
        private int identifier = 0;
        //private int health;
        private int currentHealth;
        private int maxHealth;
        
        /************************************************************************/
        /*                                                                      */
        /************************************************************************/
        public GameObject()
        {
            CurrentHealth = 100;
            MaxHealth = 100;
            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
        }
        
        public GameObject (Texture2D texture, float x, float y, Vector2 velocity, int health)
        {
            Texture = texture;
            Position = new Vector2(x, y);
            Velocity = velocity;
            setOrigin(texture);

            CurrentHealth = health;
            MaxHealth = health;
            speed = 1;
            //this.health = currentHealth;

            //textureData = new ArrayList(texture.Width * texture.Height);
        }

        /************************************************************************/
        /*  Creates a boundingbox based on the texture                          */
        /************************************************************************/
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(
                    (int)position.X - texture.Width/2,
                    (int)position.Y - texture.Height/2,
                    texture.Width,
                    texture.Height);
            }
        }


        /************************************************************************/
        /* Getters/setters for GameObject                                       */
        /************************************************************************/
        public int CurrentHealth
        {
            get { return this.currentHealth; }
            protected set { this.currentHealth = value; }
        }

        public int MaxHealth
        {
            get { return this.maxHealth; }
            protected set { this.maxHealth = value; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            protected set { this.texture = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public Vector2 Velocity
        {
            get { return this.velocity; }
            set { this.velocity = value; }
        }

        public Vector2 Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        public Vector2 AngularVelocity
        {
            get { return this.angularVelocity; }
            set { this.angularVelocity = value; }
        }

        //public ArrayList TextureData
        //{
        //    get { return textureData; }
        //}

        
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

        public float MaxSpeed
        {
            get { return this.maxSpeed; }
            set { this.maxSpeed = value; }
        }

        public float Speed
        {
            get { return this.speed; }
            set { this.speed = value; }
        }
        
        public int Identifier
        {
            get { return this.identifier; }
            set { this.identifier = value; }
        }

        public double RotationAngle
        {
            get { return this.rotationAngle; }
        }
        /************************************************************************/
        /* Other methods                                                        */
        /************************************************************************/
        public virtual void update (Vector2 velocity)
        {

            updateVelocity(velocity);
            updateAngle(velocity);
            updatePosition();
            updateRotation();
        }

        // TEMP KEYBOARD UPDATING
        public void updatePosition(float deltaX, float deltaY)
        {
            position.X = position.X + deltaX;
            position.Y = position.Y + deltaY;
        }

        public void updatePosition()
        {
            //velocity = velocity * speed;
            position.X = position.X + (velocity.X * speed);
            position.Y = position.Y - (velocity.Y * speed);
            //position += velocity;
            updateRotation();
        }

        public void updateVelocity(Vector2 velocity)
        {
            this.velocity = velocity;
            if (velocity != Vector2.Zero)
            {
                Direction = velocity;
            }
        }

        public void pushPull(Vector2 force)
        {
            velocity += force;
        }

        private void updateRotation()
        {
            rotationAngle += (double ) (Math.Atan2(angularVelocity.X, angularVelocity.Y)) * 0.1d;
            //Console.Out.WriteLine("Fittepikk");
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

        public void updateCurrentHealth(int i)
        {
            CurrentHealth += i;
        }

        protected void updateMaxHealth(int i)
        {
            MaxHealth += i;
        }

        public virtual void Draw(SpriteBatch spriteBatch, bool debug)
        {
            spriteBatch.Draw(texture, position, null, Color.White, (float)rotationAngle,
                    origin, 1.0f, SpriteEffects.None, 0f);
            
            if (debug)
                spriteBatch.Draw(rectangle, BoundingBox, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, float alpha)
        {
            spriteBatch.Draw(texture, position, null, Color.White*alpha, (float)rotationAngle,
                    origin, 1.0f, SpriteEffects.None, 0f);
        }

        public virtual void DrawScale(SpriteBatch spriteBatch, float scale, bool debug)
        {
            spriteBatch.Draw(texture, position, null, Color.White * scale, (float)rotationAngle,
                    origin, scale, SpriteEffects.None, 0f);
                
            if (debug)
                spriteBatch.Draw(rectangle, BoundingBox, Color.White);
        }


        protected void constructGameObject(Texture2D texture, float x, float y, Vector2 velocity)
        {
            Texture = texture;
            Position = new Vector2(x, y);
            Velocity = velocity;
        }
    }
}