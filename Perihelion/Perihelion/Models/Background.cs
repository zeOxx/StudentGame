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
		private Texture2D texture;
		private Texture2D[] textures;           // Used only with the second constructor
		private Vector2[] position;             // Position of the first tile
		private Vector2 prevPlayerPos;

		// Background specific variables
		private int offset;
		private int numOfTiles;                 // set in the createBackground method. Represents both x and y
		private float increment;
        private bool parallaxingObject;

		/**********************************/
		/* Constructors                   */
		/**********************************/
		public Background(Texture2D texture, Vector2 position, Vector2 prevPlayerPos, Rectangle levelBounds, float increment)
		{
			Texture = texture;
			createBackground(levelBounds, position);
			PrevPlayerPos = prevPlayerPos;
			Increment = increment;
			Offset = Texture.Width;
            ParallaxingObject = false;
		}

		// The following constructor should be used when wanting to parallax big
		//  objects (planets etc), and not entire backgrounds. The Texture2D array should
		//  be the correct number of tiles required to display the object correctly
		//  (for example, a planet that consists of 9 128x128 textures should have
		//  all it's textures passed in this array, in the CORRECT ORDER)
		public Background(Texture2D[] textures, Vector2 position, Vector2 prevPlayerPos, int tilesWidth, int tilesHeight, float increment)
		{
            createBackground(position, textures, tilesHeight, tilesWidth);
            ParallaxingObject = true;
		}

		/**********************************/
		/* Accessors                      */
		/**********************************/
		public Texture2D Texture
		{
			get { return this.texture; }
			set { this.texture = value; }
		}

        public Texture2D[] Textures
        {
            get { return this.textures; }
            set { this.textures = value; }
        }

		public Vector2[] Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		public Vector2 PrevPlayerPos
		{
			get { return this.prevPlayerPos; }
			set { this.prevPlayerPos = value; }
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
		private void createBackground(Rectangle levelBounds, Vector2 position)
		{
			numOfTiles = (levelBounds.Width + (offset * 2)) / Texture.Width;        // numOfTiles is actually number of tiles across one axis
			Position = new Vector2[(numOfTiles * numOfTiles)];
			
			for (int y = 0; y < numOfTiles; y++)
			{
				for (int x = 0; x < numOfTiles; x++)
				{
					Position[(y * numOfTiles) + x] = new Vector2(position.X + (x * Texture.Width), position.Y + (y * Texture.Height));
				}
			}
		}

        // Used in the object constructor
        private void createBackground(Vector2 position, Texture2D[] textures, int tilesHeight, int tilesWidth)
        {
            Position = new Vector2[textures.Length];

            // assigns textures correctly
            for (int i = 0; i < textures.Length; i++)
            {
                Textures[i] = textures[i];
            }

            // Positions textures accordingly
            for (int y = 0; y < tilesHeight; y++)
            {
                for (int x = 0; x < tilesWidth; x++)
                {
                    int j = (y * tilesHeight) + x;
                    Position[j] = new Vector2(position.X + (x * Textures[j].Width), position.Y + (y * Textures[j].Height));
                }
            }
        }

		private void updatePosition(Vector2 playerPos)
		{
			Vector2 tempPosition = Position[0];

			if (playerPos.X != prevPlayerPos.X)
			{
				if (playerPos.X > prevPlayerPos.X)
					tempPosition.X -= Increment;
				if (playerPos.X < prevPlayerPos.X)
					tempPosition.X += Increment;
			}

			if (playerPos.Y != prevPlayerPos.Y)
			{
				if (playerPos.Y > prevPlayerPos.Y)
					tempPosition.Y -= Increment;
				if (playerPos.Y < prevPlayerPos.Y)
					tempPosition.Y += Increment;
			}

			PrevPlayerPos = playerPos;

			for (int y = 0; y < numOfTiles; y++)
			{
				for (int x = 0; x < numOfTiles; x++)
				{
					Position[(y * numOfTiles) + x] = new Vector2(tempPosition.X + (x * Texture.Width), tempPosition.Y + (y * Texture.Height));
				}
			}
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
            // Draws the background correctly
            if (ParallaxingObject)
            {
                for (int i = 0; i < Textures.Length; i++)
                    spriteBatch.Draw(Textures[i], position[i], Color.White);
            }
            else
            {
                for (int i = 0; i < Position.Length; i++)
                    spriteBatch.Draw(Texture, Position[i], Color.White);
            }
        }
	}
}