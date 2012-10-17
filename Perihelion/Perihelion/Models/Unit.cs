using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Unit : GameObject
    {
        private int currentHealth;
        private int maxHealth;
        private float damageMultiplier;
        private float attackMultiplier;
        
        /************************************************************************/
        /*  Constructors for Player object                                      */
        /************************************************************************/
        public Unit()
            : base()
        {
            setHealth(100, 100);
            setDamageMultiplier(1);
            setAttackMultiplier(1);
        }

        public Unit(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth)
            : base(texture, x, y, velocity)
        {
            setHealth(currentHealth, maxHealth);
            setDamageMultiplier(1);
            setAttackMultiplier(1);
        }

        public Unit(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier)
            : base(texture, x, y, velocity)
        {
            setHealth(currentHealth, maxHealth);
            setDamageMultiplier(damageMultiplier);
            setAttackMultiplier(attackMultiplier);
        }

        /************************************************************************/
        /*  Set functions for Player attributes                                 */
        /************************************************************************/
        void setHealth(int currentHealth, int maxHealth)
        {
            this.currentHealth = currentHealth;
            this.maxHealth = maxHealth;
        }

        void setDamageMultiplier(float damageMultiplier)
        {
            this.damageMultiplier = damageMultiplier;
        }

        void setAttackMultiplier(float attackMultiplier)
        {
            this.attackMultiplier = attackMultiplier;
        }

        /************************************************************************/
        /*  Get functions for Player attributes                                 */
        /************************************************************************/
        int getCurrentHealth()
        {
            return this.currentHealth;
        }

        int getMaxHealth()
        {
            return this.maxHealth;
        }

        float getDamageMultiplier()
        {
            return this.damageMultiplier;
        }

        float getAttackMultiplier()
        {
            return this.attackMultiplier;
        }

        /************************************************************************/
        /*  Update functions for Unit attributes                                */
        /************************************************************************/
        void updateCurrentHealth(int i)
        {
            this.currentHealth += i;
        }

        void updateMaxHealth(int i)
        {
            this.maxHealth += i;
        }

        /************************************************************************/
        /*  Constructor functions for Unit attributes                         */
        /************************************************************************/
        protected void constructUnit(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier)
        {
            base.constructGameObject(texture, x, y, velocity);
            setHealth(currentHealth, maxHealth);
            setDamageMultiplier(damageMultiplier);
            setAttackMultiplier(attackMultiplier);
        }
    }
}
