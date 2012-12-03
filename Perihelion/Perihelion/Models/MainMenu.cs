using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Perihelion.Models
{
    class MainMenu : Menu
    {
        public bool exiting;
        public bool goPlay;
        public bool goOptions;
        public bool goCredits;

        public MainMenu(ContentHolder content, int width, int height) 
            : base(content, width, height)
        {
            Items.Add("New Game");  // itemselected = 0
            Items.Add("Options");   // itemselected = 1
            Items.Add("Credits");   // itemselected = 2
            Items.Add("Exit Game"); // itemselected = 3

            Active = true;
        }

        public bool Exiting
        {
            get { return this.exiting; }
            private set { this.exiting = value; }
        }

        public bool GoPlay
        {
            get { return this.goPlay; }
            private set { this.goPlay = value; }
        }

        public bool GoOptions
        {
            get { return this.goOptions; }
            private set { this.goOptions = value; }
        }

        public bool GoCredits
        {
            get { return this.goCredits; }
            private set { this.goCredits = value; }
        }

        // XNA METHODS
        public void update(int yAxis, bool aButton)
        {
            base.update(yAxis);

            if (aButton && ItemSelected == 0)
            {
                GoPlay = true;
            }
            else if (aButton && ItemSelected == 1)
            {
                GoOptions = true;
            }
            else if (aButton && ItemSelected == 2)
            {
                GoCredits = true;
            }
            else if (aButton && ItemSelected == 3)
            {
                Exiting = true;
            }
        }
    }
}
