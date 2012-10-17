using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Interface : GameObject
    {
        private bool enabled = true;

        // THIS CLASS EXISTS SO WE DON'T HAVE TO CALL GAMEOBJECT DIRECTLY RIGHT NOW.
        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Interface(Texture2D texture, float x, float y, Vector2 velocity) 
            : base(texture, x, y, velocity)
        {
            // nothing
            setOriginZero();
        }

        public bool Enabled
        {
            get { return this.enabled; }
            set { this.enabled = value; }
        }

        public void updateInterfacePosition()
        {

        }
    }
}
