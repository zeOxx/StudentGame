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
        private int iterator;
        public string infoText;
        private int position;
        Texture2D title;
        public bool running;

        private int inputAllowed = 500;            // Time that should elapse between input is read.
        private int elapsedSinceLastInput = 0;      // Time that has elapsed since last input.

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Menu(string derp, Texture2D title)
        {
            Title = title;
            //Font = font;

            MenuItems = new List<string>();
            MenuItems.Add("Start game");
            MenuItems.Add("Quit game");

            Iterator = 0;
            Position = 0;
            infoText = string.Empty;
            Running = false;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public Texture2D Title
        {
            get { return this.title; }
            private set { this.title = value; }
        }

        public int Iterator
        {
            get { return this.iterator; }
            private set 
            { 
                this.iterator = value; 
                
                // If tests are there to make sure the iterator can only be set to a valid value
                if (Iterator > MenuItems.Count - 1)
                    iterator = MenuItems.Count - 1;
                if (Iterator < 0)
                    iterator = 0;
            }
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
        public void update(Vector2 movement, bool aButton, GameTime gameTime)
        {
            elapsedSinceLastInput = gameTime.ElapsedGameTime.Milliseconds;

            if (elapsedSinceLastInput > inputAllowed)
            {
                // A series of if checks to make sure position is valid.
                if (movement.Y < 0)
                    Position++;
                else if (movement.Y > 0)
                    Position--;

                if (Position < 0)
                    Position = 0;
                if (Position > MenuItems.Count - 1)
                    Position = MenuItems.Count - 1;

                elapsedSinceLastInput = 0;
            }

            if (aButton)
                running = false;
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            Vector2 titlePosition = new Vector2(screenWidth / 2, screenHeight / 2);
            Vector2 itemPosition = new Vector2(titlePosition.X, titlePosition.Y + 200);

            spriteBatch.Draw(Title, titlePosition, Color.White);

            /*for (int i = 0; i < getNumberOfItems(); i++)
            {
                if (i  == Position)
                    spriteBatch.DrawString(Font, MenuItems[i], itemPosition, Color.Green);
                else
                    spriteBatch.DrawString(Font, MenuItems[i], itemPosition, Color.White);

                itemPosition.Y += 100;
            }*/
        }
    }
}
