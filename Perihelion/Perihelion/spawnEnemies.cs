using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Perihelion
{
    class spawnEnemies
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;

        public bool isVisible = true;
        Random random = new Random();
        int randX, randY;

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
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


        public bool IsVisible
        {
            get { return this.isVisible; }
            set { this.isVisible = value; }
        }

        public spawnEnemies(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;

            this.randX = random.Next(-4, 4);
            this.randY = random.Next(-4, -1);

            this.velocity = new Vector2(this.randX, this.randY);
        }

        public void update(GraphicsDevice graphics)
        {
            this.position += this.velocity;

            if (this.position.Y <= 0 || this.position.Y >= graphics.Viewport.Height - this.texture.Height)
                this.velocity.Y -= this.velocity.Y;
            if (this.position.X < 0 - this.texture.Width)
                this.isVisible = false;
        }
       
        public void draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, Color.White);
        }
    }
}
