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
        bool isDestructable;

        // These variables are only used if isDestructable is true. 
        //  If isDestructable is false, the class just generates a normal GameObject
        //int maxHealth;
        //int currentHealth;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public Collidable(Texture2D texture, float x, float y, Vector2 velocity, int health)
            : base(texture, x, y, velocity, health, health)
        {
            // CREATES A GAMEOBJECT
        }
        
        // This constructor is used if the object is destructable ONLY.
        public Collidable(Texture2D texture, float x, float y, Vector2 velocity, bool isDestructable, int maxHealth, int currentHealth, int health)
            : base(texture, x, y, velocity, currentHealth, maxHealth)
        {
            Destructable = isDestructable;
        }

        /************************************************************************/
        /* Setters                                                              */
        /************************************************************************/
        public bool Destructable
        {
            get { return this.isDestructable; }
            private set { this.isDestructable = value; }
        }

        /************************************************************************/
        /* Misc                                                                 */
        /************************************************************************/
        public void updateCurrentHealth(int healthChange)
        {
            if (healthChange < 0)
                CurrentHealth -= healthChange;
            else
                CurrentHealth += healthChange;
        }
    }
}
