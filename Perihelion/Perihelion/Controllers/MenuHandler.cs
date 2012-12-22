using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Controllers
{
    class MenuHandler
    {
        public Models.MainMenu mainMenu;
        public Models.Credits credits;
        public Models.Options options;
        public Models.EndMenu endMenu;
        public Models.Leaderboards leaderboards;

        public bool active;
        public bool exitGame;
        public bool sendScore;

        public int elapsedSinceLastInput = 100;         // defaults to the same as below, since we want users to be able to interact as soon as the game starts.
        public int allowedTimeBetweenInputs = 100;      // in ms

        public MenuHandler(ContentHolder content, int width, int height, Models.Highscores hs)
        {
            mainMenu = new Models.MainMenu(content, width, height);
            credits = new Models.Credits(content, width, height);
            options = new Models.Options(content, width, height);
            endMenu = new Models.EndMenu(content, width, height);
            leaderboards = new Models.Leaderboards(content, width, height, hs);

            sendScore = false;

            Active = true;

            exitGame = false;
        }

        // ACCESSORS
        public bool Active
        {
            get { return this.active; }
            private set { this.active = value; }
        }

        public bool ExitGame
        {
            get { return this.exitGame; }
            set { this.exitGame = value; }
        }

        public int ElapsedSinceLastInput
        {
            get { return this.elapsedSinceLastInput; }
            set { this.elapsedSinceLastInput = value; }
        }

        public int AllowedTimeBetweenInputs
        {
            get { return this.allowedTimeBetweenInputs; }
            set { this.allowedTimeBetweenInputs = value; }
        }
        
        // Update function. takes in three parameters, yAxis, aButton and bButton.
        // yAxis holds which direction to move the selector on screen
        // aButton holds wether the aButton has been pressed or not.
        public void update(int yAxis, int xAxis, bool aButton, bool bButton)
        {
            if (mainMenu.Active)
            {
                mainMenu.update(yAxis, aButton);

                if (mainMenu.Active && mainMenu.GoPlay)
                {
                    mainMenu.Active = false;
                    Active = false;
                }
                else if (mainMenu.Active && mainMenu.GoLeaderBoards)
                {
                    mainMenu.Active = false;
                    
                    if (!leaderboards.Active)
                        leaderboards.populateLeaderboard();

                    leaderboards.Active = true;
                }
                else if (mainMenu.Active && mainMenu.Exiting)
                {
                    mainMenu.Active = false;
                    Active = false;
                }
                else if (mainMenu.Active && mainMenu.GoOptions)
                {
                    mainMenu.Active = false;
                    options.Active = true;
                }
                else if (mainMenu.Active && mainMenu.GoCredits)
                {
                    mainMenu.Active = false;
                    credits.Active = true;
                }
            }
            else if (leaderboards.Active)
            {
                leaderboards.update(yAxis, bButton);

                if (!leaderboards.Active)
                {
                    mainMenu.Active = true;
                    mainMenu.goLeaderBoards = false;
                }
            }
            else if (options.Active)
            {
                options.update(yAxis, aButton);

                // If the menu becomes unactive after update:
                if (!options.Active)
                {
                    mainMenu.Active = true;
                    mainMenu.goOptions = false;
                }
            }
            else if (credits.Active)
            {
                credits.update(bButton);

                // If the menu becomes unactive after update:
                if (!credits.Active)
                {
                    mainMenu.Active = true;
                    mainMenu.goCredits = false;
                }
            }
            else if (endMenu.Active)
            {
                endMenu.update(yAxis, xAxis, aButton);

                // If endGame is finished
                if (!endMenu.Active)
                {
                    if (endMenu.sendScore)
                        sendScore = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mainMenu.Active)
                mainMenu.draw(spriteBatch);
            else if (leaderboards.Active)
                leaderboards.draw(spriteBatch);
            else if (options.Active)
                options.draw(spriteBatch);
            else if (credits.Active)
                credits.Draw(spriteBatch);
            else if (endMenu.Active)
                endMenu.draw(spriteBatch);
        }
    }
}
