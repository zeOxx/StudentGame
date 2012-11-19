using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perihelion.Models
{
    class Options
    {
        private List<string> optionsItems;
        private int position;
        public bool moved;
        public bool exiting;
        public bool playHit;
        public bool rollCredits;
        SpriteFont font;

        private int inputAllowed = 100;            // Time that should elapse between input is read.
        private int elapsedSinceLastInput = 0;      // Time that has elapsed since last input.

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public MainMenu(SpriteFont font)
        {
            Font = font;

            // Main menu items 
            OptionsItems = new List<string>();
            OptionsItems.Add("Start game");
            OptionsItems.Add("Options");
            OptionsItems.Add("Credits");
            OptionsItems.Add("Quit game");

            Exiting = false;
            PlayHit = false;
            Moved = false;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public List<string> OptionsItems
        {
            get { return this.optionsItems; }
            private set { this.optionsItems = value; }
        }

        public int Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public bool Moved
        {
            get { return this.moved; }
            private set { this.moved = value; }
        }

        public bool Exiting
        {
            get { return this.exiting; }
            private set { this.exiting = value; }
        }

        public bool PlayHit
        {
            get { return this.playHit; }
            set { this.playHit = value; }
        }

        public bool RollCredits
        {
            get { return this.rollCredits; }
            set { this.rollCredits = value; }
        }

        public SpriteFont Font
        {
            get { return this.font; }
            private set { this.font = value; }
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(int movement, bool aButton, GameTime gameTime)
        {
            elapsedSinceLastInput += gameTime.ElapsedGameTime.Milliseconds;

            // A series of if checks to make sure position is valid.
            if (movement < 0)
            {
                Position--;
                Moved = true;
            }
            else if (movement > 0)
            {
                Position++;
                Moved = true;
            }
            else
                Moved = false;

            // This gate is here to prevent unwanted movement
            if (elapsedSinceLastInput > inputAllowed)
            {
                if (Position < 0)
                    Position = 0;
                if (Position > OptionsItems.Count - 1)
                    Position = OptionsItems.Count - 1;
                if (Position > OptionsItems.Count - 1)
                    Position = OptionsItems.Count - 1;

                if (aButton && Position == 0)
                    PlayHit = true;

                if (aButton && Position == 2)
                    RollCredits = true;

                if (aButton && Position == (OptionsItems.Count - 1))
                    Exiting = true;
            }

            if (moved)
                elapsedSinceLastInput = 0;
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight, int iterator, Vector2 titlePosition)
        {
            for (int i = 0; i < OptionsItems.Count; i++)
            {
                Vector2 centerText = Font.MeasureString(OptionsItems[i]);

                Vector2 itemPosition = new Vector2((screenWidth / 2 - centerText.X / 2), titlePosition.Y + iterator);

                if (i == Position)
                    spriteBatch.DrawString(Font, OptionsItems[i], itemPosition, Color.Green);
                else
                    spriteBatch.DrawString(Font, OptionsItems[i], itemPosition, Color.White);

                iterator += 75;
            }
        }
    }
}
