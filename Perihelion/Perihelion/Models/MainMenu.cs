using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class MainMenu
    {
        private List<string> mainMenuItems;
        private int itemSelected;
        public bool moved;
        public bool exiting;
        public bool playHit;
        public bool intoOptions;
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
            MainMenuItems = new List<string>();
            MainMenuItems.Add("Start game");
            MainMenuItems.Add("Options");
            MainMenuItems.Add("Credits");
            MainMenuItems.Add("Quit game");

            Exiting = false;
            IntoOptions = false;
            PlayHit = false;
            Moved = false;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public List<string> MainMenuItems
        {
            get { return this.mainMenuItems; }
            private set { this.mainMenuItems = value; }
        }

        public int ItemSelected
        {
            get { return this.itemSelected; }
            private set { this.itemSelected = value; }
        }

        public bool Moved
        {
            get { return this.moved; }
            private set { this.moved = value; }
        }

        public bool Exiting
        {
            get { return this.exiting; }
            set { this.exiting = value; }
        }

        public bool PlayHit
        {
            get { return this.playHit; }
            set { this.playHit = value; }
        }

        public bool IntoOptions
        {
            get { return this.intoOptions; }
            set { this.intoOptions = value; }
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
                ItemSelected--;
                Moved = true;
            }
            else if (movement > 0)
            {
                ItemSelected++;
                Moved = true;
            }
            else
                Moved = false;

            // This gate is here to prevent unwanted movement
            if (elapsedSinceLastInput > inputAllowed)
            {
                if (ItemSelected < 0)
                    ItemSelected = 0;
                if (ItemSelected > MainMenuItems.Count - 1)
                    ItemSelected = MainMenuItems.Count - 1;

                if (aButton && ItemSelected == 0)
                    PlayHit = true;

                if (aButton && ItemSelected == 1)
                    IntoOptions = true;

                if (aButton && ItemSelected == 2)
                    RollCredits = true;

                if (aButton && ItemSelected == (MainMenuItems.Count - 1))
                    Exiting = true;
            }

            if (moved)
                elapsedSinceLastInput = 0;
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight, int iterator, Vector2 titlePosition)
        {
            for (int i = 0; i < MainMenuItems.Count; i++)
            {
                Vector2 centerText = Font.MeasureString(MainMenuItems[i]);

                Vector2 itemPosition = new Vector2((screenWidth / 2 - centerText.X / 2), titlePosition.Y + iterator);

                if (i == ItemSelected)
                    spriteBatch.DrawString(Font, MainMenuItems[i], itemPosition, Color.Green);
                else
                    spriteBatch.DrawString(Font, MainMenuItems[i], itemPosition, Color.White);

                iterator += 75;
            }
        }
    }
}
