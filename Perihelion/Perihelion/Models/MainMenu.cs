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
        private int position;
        public bool moved;
        public bool exiting;
        public bool playHit;
        SpriteFont font;

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
            PlayHit = false;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public List<string> MainMenuItems
        {
            get { return this.mainMenuItems; }
            private set { this.mainMenuItems = value; }
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
            private set { this.playHit = value; }
        }

        public SpriteFont Font
        {
            get { return this.font; }
            private set { this.font = value; }
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(int movement, bool aButton)
        {
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

            if (Position < 0)
                Position = 0;
            if (Position > MainMenuItems.Count - 1)
                Position = MainMenuItems.Count - 1;
            if (Position > MainMenuItems.Count - 1)
                Position = MainMenuItems.Count - 1;

            if (aButton && Position == 0)
                PlayHit = true;

            if (aButton && Position == (MainMenuItems.Count - 1))
                Exiting = true;
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight, int iterator, Vector2 titlePosition)
        {
            for (int i = 0; i < MainMenuItems.Count; i++)
            {
                Vector2 centerText = Font.MeasureString(MainMenuItems[i]);

                Vector2 itemPosition = new Vector2((screenWidth / 2 - centerText.X / 2), titlePosition.Y + iterator);

                if (i == Position)
                    spriteBatch.DrawString(Font, MainMenuItems[i], itemPosition, Color.Green);
                else
                    spriteBatch.DrawString(Font, MainMenuItems[i], itemPosition, Color.White);

                iterator += 75;
            }
        }
    }
}
