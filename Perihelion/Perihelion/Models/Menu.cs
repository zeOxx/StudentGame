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
        public string title;
        private int position;
        SpriteFont font;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Menu(string title, SpriteFont font)
        {
            Title = title;
            Font = font;

            MenuItems = new List<string>();
            MenuItems.Add("Start game");
            MenuItems.Add("Quit game");

            Iterator = 0;
            Position = 0;
            infoText = string.Empty;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public string Title
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

        public SpriteFont Font
        {
            get { return this.font; }
            private set { this.font = value; }
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

        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight, SpriteFont arial)
        {
            Vector2 firstPosition = new Vector2(screenWidth / 2, screenHeight / 2);
            Vector2 nextPosition = firstPosition;

            spriteBatch.DrawString(Font, Title, firstPosition, Color.White);

            for (int i = 0; i < getNumberOfItems(); i++)
            {
                nextPosition.Y += 100;
                spriteBatch.DrawString(Font, MenuItems[i], nextPosition, Color.White);
            }
        }
    }
}
