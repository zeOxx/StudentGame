using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Perihelion.Models
{
    class Menu
    {
        private List<string> menuItems;
        public string infoText;
        private int position;
        public bool running;
        public bool moved;
        public bool exiting;
        Texture2D title;
        SpriteFont font;

        private int inputAllowed = 100;            // Time that should elapse between input is read.
        private int elapsedSinceLastInput = 100;      // Time that has elapsed since last input.

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Menu(ContentHolder contentHolder)
        {
            Title = contentHolder.title;
            Font = contentHolder.menuFont;

            MenuItems = new List<string>();
            MenuItems.Add("Start game");
            MenuItems.Add("Options");
            MenuItems.Add("Credits");
            MenuItems.Add("Quit game");

            Position = 0;
            infoText = string.Empty;
            Running = true;
            Exiting = false;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public Texture2D Title
        {
            get { return this.title; }
            private set { this.title = value; }
        }

        public SpriteFont Font
        {
            get { return this.font; }
            private set { this.font = value; }
        }

        public List<string> MenuItems
        {
            get { return this.menuItems; }
            private set { this.menuItems = value; }
        }

        public int Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public bool Running
        {
            get { return this.running; }
            private set { this.running = value; }
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

        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        public int getNumberOfItems()
        {
            return MenuItems.Count;
        }

        public string getItem(int index)
        {
            return MenuItems[index];
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(int movement, bool aButton, GameTime gameTime)
        {
            elapsedSinceLastInput += gameTime.ElapsedGameTime.Milliseconds;

            // This gate is here to prevent unwanted movement
            if (elapsedSinceLastInput > inputAllowed)
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
                if (Position > MenuItems.Count - 1)
                    Position = MenuItems.Count - 1;

                if (Moved)
                    elapsedSinceLastInput = 0;
            }

            if (aButton && Position == 0)
                running = false;

            if (aButton && Position == (MenuItems.Count - 1))
                Exiting = true;
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            Vector2 titlePosition = new Vector2(((screenWidth / 2) - (title.Width / 2)), (150 - (title.Height / 2)));
            int iterator = (int)titlePosition.Y + 50;
            spriteBatch.Draw(Title, titlePosition, Color.White);

            for (int i = 0; i < getNumberOfItems(); i++)
            {
                Vector2 centerText = Font.MeasureString(MenuItems[i]);

                Vector2 itemPosition = new Vector2((screenWidth / 2 - centerText.X / 2), titlePosition.Y + iterator);

                if (i  == Position)
                    spriteBatch.DrawString(Font, MenuItems[i], itemPosition, Color.Green);
                else
                    spriteBatch.DrawString(Font, MenuItems[i], itemPosition, Color.White);

                iterator += 75;
            }
        }
    }
}
