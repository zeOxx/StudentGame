using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Collidable : GameObject
    {
        bool isDestructible;

        // Health variables are only used if isDestructable is true. 
        //  If isDestructable is false, the class just generates a normal GameObject

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Collidable(Texture2D texture, float x, float y, Vector2 velocity, int health)
            : base(texture, x, y, velocity, health)
        {
            // CREATES A GAMEOBJECT
        }
        
        // This constructor is used if the object is destructible ONLY.
        public Collidable(Texture2D texture, float x, float y, Vector2 velocity, bool isDestructible, int health)
            : base(texture, x, y, velocity, health)
        {
            Destructible = isDestructible;
        }

        /************************************************************************/
        /* Setters                                                              */
        /************************************************************************/
        public bool Destructible
        {
            get { return this.isDestructible; }
            private set { this.isDestructible = value; }
        }
    }
}
