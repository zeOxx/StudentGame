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
        private float accelerationMultiplier = 0.02f;
        
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
        public void unitUpdate (Vector2 velocity)
        {
            base.update(scaleVelocity(velocity));
        }

        void updateCurrentHealth(int i)
        {
            this.currentHealth += i;
        }

        void updateMaxHealth(int i)
        {
            this.maxHealth += i;
        }

        private Vector2 scaleVelocity (Vector2 velocity)
        {
            float oldlength = this.velocity.Length();
            float newlength = velocity.Length();
            Console.Out.WriteLine("old {0}", oldlength);
            Console.Out.WriteLine("new {0}", newlength);

            if (newlength > (oldlength + accelerationMultiplier))
            {
                if ((oldlength + accelerationMultiplier) <= 1)
                {
                    speed = maxSpeed * (oldlength + accelerationMultiplier);
                    velocity = velocity / velocity.Length() * (oldlength + accelerationMultiplier);
                }
                else
                {
                    speed = maxSpeed;
                }
                Console.Out.WriteLine("too damn high {0}", speed);
                return velocity;
            }
            else if (newlength < (oldlength - accelerationMultiplier))
            {
                speed = maxSpeed * (oldlength - accelerationMultiplier);
                if (velocity.Length() != 0)
                {
                    velocity = velocity / velocity.Length() * (oldlength - accelerationMultiplier);
                    Console.Out.WriteLine("too low but still goin strong {0}", speed);
                }
                else
                {
                    velocity = this.velocity/ oldlength * (oldlength - accelerationMultiplier);
                    Console.Out.WriteLine("too low but still goin strong {0}", speed);
                }
                return velocity;
            }
            else
            {
                speed = newlength * maxSpeed;
                Console.Out.WriteLine("just right {0}", speed);
                return velocity;
            }
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
