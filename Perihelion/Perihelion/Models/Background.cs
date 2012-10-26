using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
	class Background
	{
		// XNA types
		private Texture2D[] textures;           // Stores an array of textures.
		private Vector2 prevPlayerPos;
        
        // Background specific variables
		private int offset;
        private int tilesWidth;
        private int tilesHeight;
		private int numOfTiles;
		private float increment;
        private bool parallaxingObject;
        private Random random = new Random();

        // fRectangle struct
        public struct fRectangle {
            public Vector2 position;                 // An array of positions for all the tiles in the fRectangle.
            public int width;
            public int height;

            public fRectangle(Vector2 newPos, int newWidth, int newHeight)
            {
                position = newPos;
                width = newWidth;
                height = newHeight;
            }
        };
        private fRectangle backgroundRect;

		/**********************************/
		/* Constructors                   */
		/**********************************/
        public Background(Texture2D[] textures, Vector2 position, Vector2 velocity, Rectangle levelBounds, int width, int height, float increment)
		{
            Offset = (textures[0].Width * 2);
            setRectangle(levelBounds);
            setTextures(textures, width, height);
			PrevPlayerPos = prevPlayerPos;
			Increment = increment;
            ParallaxingObject = false;
		}

		/**********************************/
		/* Accessors                      */
		/**********************************/
        public Texture2D[] Textures
        {
            get { return this.textures; }
            set { this.textures = value; }
        }

		public Vector2 PrevPlayerPos
		{
			get { return this.prevPlayerPos; }
			set { this.prevPlayerPos = value; }
		}

        public int TilesHeight
        {
            get { return this.tilesHeight; }
            set { this.tilesHeight = value; }
        }

        public int TilesWidth
        {
            get { return this.tilesWidth; }
            set { this.tilesWidth = value; }
        }

		public int Offset
		{
			get { return this.offset; }
			set { this.offset = value; }
		}

		public float Increment
		{
			get { return this.increment; }
			set { this.increment = value; }
		}

        public bool ParallaxingObject
        {
            get { return this.parallaxingObject; }
            set { this.parallaxingObject = value; }
        }

		/**********************************/
		/* Methods                        */
		/**********************************/
		private void updatePosition(Vector2 velocity)
		{
            backgroundRect.position.X += velocity.X * Increment;
            backgroundRect.position.Y -= velocity.Y * Increment;
		}

        // Sets textures correctly.
        private void setTextures(Texture2D[] textures, int width, int height)
        {
            if (width == 0)
            {
                TilesWidth = backgroundRect.width / textures[0].Width;
                TilesHeight = backgroundRect.height / textures[0].Height;
            }
            else
            {
                TilesWidth = width;
                TilesHeight = height;
            }

            numOfTiles = TilesWidth * TilesHeight;

            Textures = new Texture2D[numOfTiles];
            int randomNumber;

            for (int i = 0; i < Textures.Length; i++) 
            {
                randomNumber = random.Next(textures.Length);
                Textures[i] = textures[randomNumber];
            }
                
        }

        private void setRectangle(Rectangle levelBounds)
        {
            backgroundRect = new fRectangle(new Vector2(levelBounds.X - Offset, levelBounds.Y - Offset), levelBounds.Width + (Offset * 2), levelBounds.Height + (Offset* 2));
        }
		/**********************************/
		/* XNA Methods                    */
		/**********************************/
		public void update(Vector2 playerPos)
		{
			updatePosition(playerPos);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
            Vector2 tempPosition;
            float textureHeight = Textures[0].Height;
            float textureWidth = Textures[0].Width;

            for (int y = 0; y < TilesHeight; y++)
            {
                for (int x = 0; x < TilesWidth; x++)
                {
                    tempPosition = new Vector2(backgroundRect.position.X + (textureWidth * x), backgroundRect.position.Y + (textureHeight * y));

                    spriteBatch.Draw(Textures[x + (y * TilesWidth)], tempPosition, Color.White);
                }
            }
        }
	}
}