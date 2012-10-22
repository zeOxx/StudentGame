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
		private Vector2[] position;             // Position of the first tile
		private Vector2 prevPlayerPos;

		// Background specific variables
		private int offset;
		private int numOfTiles;                 // set in the createBackground method. Represents both x and y
		private float increment;

		/**********************************/
		/* Constructor                    */
		/**********************************/
		public Background(Texture2D texture, Vector2 position, Vector2 prevPlayerPos, Rectangle levelBounds, float increment)
		{
			Texture = texture;
			createBackground(levelBounds, position);
			PrevPlayerPos = prevPlayerPos;
			Increment = increment;
			Offset = Texture.Width;
		}

		/**********************************/
		/* Accessors                      */
		/**********************************/
		public Texture2D Texture
		{
			get { return this.texture; }
			set { this.texture = value; }
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
		/**********************************/
		/* Methods                        */
		/**********************************/
		private void createBackground(Rectangle levelBounds, Vector2 position)
		{
			numOfTiles = (levelBounds.Width + (offset * 2)) / Texture.Width;
			this.position = new Vector2[(numOfTiles * numOfTiles)];
			
			for (int y = 0; y < numOfTiles; y++)
			{
				for (int x = 0; x < numOfTiles; x++)
				{
					this.position[(y * numOfTiles) + x] = new Vector2(position.X + (x * Texture.Width), position.Y + (y * Texture.Height));
				}
			}

			Console.Out.WriteLine(position);
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
			for (int i = 0; i < Position.Length; i++)
				spriteBatch.Draw(Texture, Position[i], Color.White);
		}
	}
}