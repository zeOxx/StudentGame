using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Credits : Menu
    {
        public Vector2[] position;
        public float[] alpha;
        SpriteFont creditsFont;
        SpriteFont headerFont;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Credits(ContentHolder content, int width, int height)
            : base(content, width, height)
        {
            Items.Add("Everything");
            Items.Add("Inge Dalby");
            Items.Add("Simen Løkken");
            Items.Add("Snorre Brecheisen");
            Items.Add("Thomas Nilsen");

            HeaderFont = content.creditsHeaderFont;
            CreditsFont = content.creditsFont;
            Active = false;
            Position = new Vector2[5];
            Alpha = new float[5];
            setPositions();
            setAlphas();
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public Vector2[] Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public float[] Alpha
        {
            get { return this.alpha; }
            private set { this.alpha = value; }
        }

        public SpriteFont HeaderFont
        {
            get { return this.headerFont; }
            private set { this.headerFont = value; }
        }

        public SpriteFont CreditsFont
        {
            get { return this.creditsFont; }
            private set { this.creditsFont = value; }
        }

        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        private void setPositions()
        {
            Vector2 centerText = HeaderFont.MeasureString(Items[0]);
            Position[0] = new Vector2((ScreenWidth / 2 - centerText.X / 2), ScreenHeight);
            centerText = CreditsFont.MeasureString(Items[1]);
            Position[1] = new Vector2((ScreenWidth / 2 - centerText.X / 2), ScreenHeight + 50);
            centerText = CreditsFont.MeasureString(Items[2]);
            Position[2] = new Vector2((ScreenWidth / 2 - centerText.X / 2), ScreenHeight + 80);
            centerText = CreditsFont.MeasureString(Items[3]);
            Position[3] = new Vector2((ScreenWidth / 2 - centerText.X / 2), ScreenHeight + 110);
            centerText = CreditsFont.MeasureString(Items[4]);
            Position[4] = new Vector2((ScreenWidth / 2 - centerText.X / 2), ScreenHeight + 140);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Title, TitlePosition, Color.White);

            for (int i = 0; i < Items.Count; i++)
            {
                if (i == 0)
                    spriteBatch.DrawString(HeaderFont, Items[i], Position[i], Color.White * Alpha[i]);
                else
                    spriteBatch.DrawString(CreditsFont, Items[i], Position[i], Color.White * Alpha[i]);

                // Handles fading
                if ((Position[i].Y < (ScreenHeight / 2) - 100))
                    Alpha[i] -= 0.05f;
            }

            for (int i = 0; i < Position.Length; i++)
            {
                Position[i].Y -= 1;
            }
        }
    }
}
