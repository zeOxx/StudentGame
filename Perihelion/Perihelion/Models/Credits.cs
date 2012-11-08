using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Credits
    {
        private List<string> creditsMenuItems;
        public bool active;
        public Vector2[] position;
        public int width;
        public int height;
        public float[] alpha;
        SpriteFont font;
        SpriteFont headerFont;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Credits(SpriteFont font, SpriteFont headerFont, int screenWidth, int screenHeight)
        {
            Font = font;
            HeaderFont = headerFont;

            // Credits
            CreditsMenuItems = new List<string>();
            CreditsMenuItems.Add("Everything");
            CreditsMenuItems.Add("Inge Dalby");
            CreditsMenuItems.Add("Simen Løkken");
            CreditsMenuItems.Add("Snorre Brecheisen");
            CreditsMenuItems.Add("Thomas Nilsen");

            Active = true;
            Position = new Vector2[5];
            Alpha = new float[5];
            Width = screenWidth;
            Height = screenHeight;
            setPositions();
            setAlphas();
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public List<string> CreditsMenuItems
        {
            get { return this.creditsMenuItems; }
            private set { this.creditsMenuItems = value; }
        }

        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        public Vector2[] Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public int Width
        {
            get { return this.width; }
            private set { this.width = value; }
        }

        public int Height
        {
            get { return this.height; }
            private set { this.height = value; }
        }

        public float[] Alpha
        {
            get { return this.alpha; }
            private set { this.alpha = value; }
        }

        public SpriteFont Font
        {
            get { return this.font; }
            private set { this.font = value; }
        }

        public SpriteFont HeaderFont
        {
            get { return this.headerFont; }
            private set { this.headerFont = value; } 
        }

        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        private void setPositions()
        {
            Vector2 centerText = HeaderFont.MeasureString(CreditsMenuItems[0]);
            Position[0] = new Vector2((Width / 2 - centerText.X / 2), Height);
            centerText = Font.MeasureString(CreditsMenuItems[1]);
            Position[1] = new Vector2((Width / 2 - centerText.X / 2), Height + 50);
            centerText = Font.MeasureString(CreditsMenuItems[2]);
            Position[2] = new Vector2((Width / 2 - centerText.X / 2), Height + 80);
            centerText = Font.MeasureString(CreditsMenuItems[3]);
            Position[3] = new Vector2((Width / 2 - centerText.X / 2), Height + 110);
            centerText = Font.MeasureString(CreditsMenuItems[4]);
            Position[4] = new Vector2((Width / 2 - centerText.X / 2), Height + 140);
        }

        private void setAlphas()
        {
            for (int i = 0; i < Alpha.Length; i++)
            {
                Alpha[i] = 1.0f;
            }
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(bool bButton)
        {
            if (bButton)
                Active = false;

            // Resets everything if the screen is no longer active
            if (!Active)
            {
                setPositions();
                setAlphas();
            }
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            for (int i = 0; i < CreditsMenuItems.Count; i++)
            {
                if (i == 0)
                    spriteBatch.DrawString(HeaderFont, CreditsMenuItems[i], Position[i], Color.White * Alpha[i]);
                else
                    spriteBatch.DrawString(Font, CreditsMenuItems[i], Position[i], Color.White * Alpha[i]);

                // Handles fading
                if ((Position[i].Y < (Height /2) - 100))
                    Alpha[i] -= 0.05f;
            }

            for (int i = 0; i < Position.Length; i++)
            {
                Position[i].Y -= 1;
            }
        }
    }
}
