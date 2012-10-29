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
            spriteBatch.Draw(Texture, Position, null, Color.White, Rotation, origin, 1.0f, SpriteEffects.None, 0f);
            // spriteBatch.Draw(Texture, Position, null, new Color(255, 255, 255, (byte)MathHelper.Clamp(alphaValue, 0, 255)), Rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
