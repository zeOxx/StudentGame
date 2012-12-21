using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Options : Menu
    {
        public bool fullscreen; // NOT IN USE AT THE MOMENT
        public int offsetX;     // NOT IN USE AT THE MOMENT

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Options(ContentHolder content, int width, int height)
            : base(content, width, height)
        {
            Items = new List<string>();
            Items.Add("Fullscreen");    // ItemSelected 0
            Items.Add("Resolution");    // ItemSelected 1
            Items.Add("Back");          // ItemSelected 2

            OffsetX = 50;

            Active = false;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public bool Fullscreen
        {
            get { return this.fullscreen; }
            private set { this.fullscreen = value; }
        }

        public int OffsetX
        {
            get { return this.offsetX; }
            private set { this.offsetX = value; }
        }
        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(int yAxis, bool aButton)
        {
            base.update(yAxis);

            if (aButton && ItemSelected == 0)
                Fullscreen = true;
            else if (aButton && ItemSelected == 2)
                Active = false;
        }
    }
}
