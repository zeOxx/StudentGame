using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class Player : Unit
    {
        protected Texture2D texture_turret;
        protected double turretRotationAngle = 0.0;
        
        private float wellMultiplier;
        private int wellStatus;
        private int auxiliaryPower;   

        /************************************************************************/
        /*  Constructors for Player object                                      */
        /************************************************************************/
        public Player()
            : base()
        {
            setWellMultiplier(1);
            setWellStatus(0);
            setAuxiliaryPower(100);
        }

        public Player(Texture2D texture_ship, Texture2D texture_turret, float x, float y, Vector2 velocity, int currentHealth, int maxHealth)
            : base(texture_ship, x, y, velocity, currentHealth, maxHealth)
        {
            setTurretTexture(texture_turret);
            setWellMultiplier(1);
            setWellStatus(0);
            setAuxiliaryPower(100);

            // Temp
            setSpeed(5);
            setMaxSpeed(5);
        }

        public Player(Texture2D texture, Texture2D texture_turret, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier, float wellMultiplier, int wellStatus, int auxiliaryPower)
            : base(texture, x, y, velocity, currentHealth, maxHealth, damageMultiplier, attackMultiplier)
        {
            setTurretTexture(texture_turret);
            setWellMultiplier(wellMultiplier);
            setWellStatus(wellStatus);
            setAuxiliaryPower(auxiliaryPower);
        }
        /************************************************************************/
        /*  Set functions for Player attributes                                 */
        /************************************************************************/
        void setTurretTexture(Texture2D texture_turret)
        {
            this.texture_turret = texture_turret;
        }
        
        void setWellMultiplier(float wellModifier)
        {
            this.wellMultiplier = wellModifier;
        }

        void setWellStatus(int wellStatus)
        {
            this.wellStatus = wellStatus;
        }

        void setAuxiliaryPower(int auxiliaryPower)
        {
            this.auxiliaryPower = auxiliaryPower;
        }

        /************************************************************************/
        /*  Get functions for Player attributes                                 */
        /************************************************************************/
        float getWellMultiplier()
        {
            return this.wellMultiplier;
        }

        int getWellStatus()
        {
            return this.wellStatus;
        }

        int getAuxiliaryPower()
        {
            return this.auxiliaryPower;
        }

        /************************************************************************/
        /*  Update functions for Player attributes                              */
        /************************************************************************/
        public void update(Vector2 velocity, Vector2 rightStick)
        {
            updateTurretAngle(rightStick);
            base.update(velocity);
        }

        public void updateWellMultiplier(float i)
        {
            this.wellMultiplier += i;
        }

        public void updateAuxiliaryPower(int i)
        {
            this.auxiliaryPower += i;
        }

        public void updateTurretAngle(Vector2 rightStick)
        {
            if (rightStick.X != 0.0f && rightStick.Y != 0.0f)
                
                turretRotationAngle = Math.Atan2((double)rightStick.X, (double)rightStick.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture_turret, position, null, Color.White, (float)turretRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }
        

        // WUT
//         /************************************************************************/
//         /*  Constructor functions for Player attributes                         */
//         /************************************************************************/
// 
//         public void constructPlayer(Texture2D texture, float x, float y, Vector2 velocity, int currentHealth, int maxHealth, float damageMultiplier, float attackMultiplier, float wellMultiplier, int wellStatus, int auxiliaryPower)
//         {
//             base.constructUnit(texture, x, y, velocity, currentHealth, maxHealth, damageMultiplier, attackMultiplier);
//             setWellMultiplier(wellMultiplier);
//             setWellStatus(wellStatus);
//             setAuxiliaryPower(auxiliaryPower);
//         }
    }
}
