using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Particle
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 origin;

        private int life;                   // Length of the particle's life, in MS
        private int totalLife;              // Time that has elapsed in the particles lifetime
        private float rotation;
        private float speed;
        private int startFading;                    // Tells the class when to start fading. this is set further down to half of a particles life.

        // Fade values
        private int fadeIncrement = 22;
        private int fadeAmount = 255;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, int life, float rotation, float speed)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Life = life;
            Rotation = rotation;
            Speed = speed;

            setStartFading();

            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public Texture2D Texture
        {
            get { return this.texture; }
            private set { this.texture = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public Vector2 Velocity
        {
            get { return this.velocity; }
            private set { this.velocity = value; }
        }

        public Vector2 Origin
        {
            get { return this.origin; }
            private set { this.origin = value; }
        }

        public int Life
        {
            get { return this.life; }
            private set { this.life = value; }
        }

        public int TotalLife
        {
            get { return this.totalLife; }
            private set { this.totalLife = value; }
        }

        public float Rotation
        {
            get { return this.rotation; }
            private set { this.rotation = value; }
        }

        public float Speed
        {
            get { return this.speed; }
            private set { this.speed = value; }
        }

        public int StartFading
        {
            get { return this.startFading; }
            private set { this.startFading = value; }
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        private void setStartFading()
        {
            startFading = Life / 2;
        }

        public void fade()
        {
            fadeAmount -= fadeIncrement;
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(GameTime gameTime)
        {
            TotalLife += gameTime.ElapsedGameTime.Milliseconds;

            position.X = Position.X + (Velocity.X * Speed);
            position.Y = Position.Y - (Velocity.Y * Speed);
        } 

        public void Draw(SpriteBatch spriteBatch)
        {
             //spriteBatch.Draw(Texture, Position, null, new Color(255, 255, 255, (byte)MathHelper.Clamp(fadeAmount, 0, 255)), Rotation, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Texture, Position, null, Color.Lerp(Color.White, Color.Transparent, fadeAmount), Rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
