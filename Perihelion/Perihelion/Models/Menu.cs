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
        public bool running;
        public bool exiting;
        Texture2D title;
        SpriteFont font;

        public enum MenuStates
        {
            Main,
            Options,
            Credits
        }

        public MenuStates menuState;

        MainMenu mainMenu;
        Options options;
        Credits credits;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Menu(ContentHolder contentHolder, int screenWidth, int screenHeight)
        {
            menuState = MenuStates.Main;

            Title = contentHolder.title;
            Font = contentHolder.menuFont;

            mainMenu = new MainMenu(Font);
            options = new Options(Font);
            credits = new Credits(contentHolder.creditsFont, contentHolder.creditsHeaderFont, screenWidth, screenHeight);

            // Options menu items
            //OptionsMenuItems = new List<string>();
            //OptionsMenuItems.Add("Music");
            //OptionsMenuItems.Add("Sound");
            //OptionsMenuItems.Add("Hints");

            Running = true;
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

        public bool Running
        {
            get { return this.running; }
            private set { this.running = value; }
        }

        public bool Exiting
        {
            get { return this.exiting; }
            private set { this.exiting = value; }
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(int movement, bool aButton, bool bButton, GameTime gameTime)
        {
            // Runs only if the player is in the main menu
            if (menuState == MenuStates.Main)
            {
                mainMenu.update(movement, aButton, gameTime);

                if (mainMenu.PlayHit)
                {
                    Running = false;
                    mainMenu.PlayHit = false;
                }

                if (mainMenu.Exiting)
                    Exiting = true;

                if (mainMenu.IntoOptions)
                    menuState = MenuStates.Options;

                if (mainMenu.RollCredits)
                    menuState = MenuStates.Credits;
            }
            else if (menuState == MenuStates.Credits)
            {
                credits.Active = true;

                credits.update(bButton);

                if (!credits.Active)
                {
                    menuState = MenuStates.Main;
                    mainMenu.RollCredits = false;
                }
            }
            else if (menuState == MenuStates.Options)
            {
                options.Active = true;

                options.update(movement, aButton, gameTime);

                if (options.Exiting)
                {
                    options.Active = false;
                    menuState = MenuStates.Main;
                    mainMenu.Exiting = false;
                    mainMenu.IntoOptions = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            Vector2 titlePosition = new Vector2(((screenWidth / 2) - (title.Width / 2)), (150 - (title.Height / 2)));
            int iterator = (int)titlePosition.Y + 50;
            spriteBatch.Draw(Title, titlePosition, Color.White);

            if (menuState == MenuStates.Main)
            {
                mainMenu.Draw(spriteBatch, screenWidth, screenHeight, iterator, titlePosition);
            }

            if (menuState == MenuStates.Options)
            {
                options.Draw(spriteBatch, screenWidth, screenHeight, iterator, titlePosition);
            }

            if (menuState == MenuStates.Credits)
            {
                credits.Draw(spriteBatch, screenWidth, screenHeight);
            }
        }
    }
}
