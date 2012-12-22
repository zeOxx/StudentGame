using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Perihelion
{
    class HUD
    {
        /************************************************************************/
        /* Preparing objects                                                    */
        /************************************************************************/
        Models.Interface healthAuxBar;
        Models.Interface special;

        /************************************************************************/
        /* Positional vectors                                                   */
        /************************************************************************/
        private Vector2 healthAuxBarPosition;
        private Vector2 specialPosition;
        private Vector2 specialAmmoPosition;
        private SpriteFont ammoFont;

        /************************************************************************/
        /* Strings                                                              */
        /************************************************************************/
        private String ammoCounterSpecial;

#if DEBUG
        /************************************************************************/
        /* Debug variables                                                      */
        /************************************************************************/
        String playerHealth = "Player health: ";
        Vector2 playerHealthVector;
        String playerPosition = "Player position";
        Vector2 playerPositionVector;
        String numberOfBullets = "Number of bullets: ";
        Vector2 numberOfBulletsVector;
        String playerSpeed = "Player speed: ";
        Vector2 playerSpeedVector;
        SpriteFont debugFont;
        bool displayDebug;
#endif

        private int margin = 10;
        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public HUD(ContentHolder contentHolder, Camera camera)
        {
            updateHudPositions(camera);

            healthAuxBar = new Models.Interface(contentHolder.healthAuxBar, healthAuxBarPosition.X, healthAuxBarPosition.Y, new Vector2(0, 0), 0);
            special = new Models.Interface(contentHolder.special, specialPosition.X, specialPosition.Y, new Vector2(0, 0), 0);

            ammoFont = contentHolder.creditsFont;
            ammoCounterSpecial = "deo";
        
#if DEBUG  
            this.debugFont = contentHolder.debugFont;
#endif
        }

        /************************************************************************/
        /* Accessors this doesn't compile in Release                                                            */
        /************************************************************************/
        public bool DisplayDebug
        {
            get { return this.displayDebug; }
            set { this.displayDebug = value; }
        }
        
        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        public void update(Models.Player player)
        {
            healthAuxBar.Position = new Vector2(healthAuxBarPosition.X, healthAuxBarPosition.Y);
            special.Position = new Vector2(specialPosition.X, specialPosition.Y);

            ammoCounterSpecial = player.RocketAmmo.ToString();
            // Calls update to all the objects to check health etc for the player. Yet to be implemented
        
#if DEBUG
            if (displayDebug)
            {
                playerPosition = "Player position: " + player.Position;
                numberOfBullets = "Number of bullets: " + player.BulletList.Count;
                playerSpeed = "Player speed: " + player.Speed;
            }
#endif
        }

        public void Draw(SpriteBatch spriteBatch, bool debug)
        {
            healthAuxBar.Draw(spriteBatch, debug);
            special.Draw(spriteBatch, debug);

            spriteBatch.DrawString(ammoFont, ammoCounterSpecial, specialAmmoPosition, Color.White);
#if DEBUG
            if (displayDebug)
            {
                spriteBatch.DrawString(debugFont, playerPosition, playerPositionVector, Color.White);
                spriteBatch.DrawString(debugFont, numberOfBullets, numberOfBulletsVector, Color.White);
                spriteBatch.DrawString(debugFont, playerSpeed, playerSpeedVector, Color.White);
            }
#endif
        }

        public void updateHudPositions(Camera camera)
        {
            healthAuxBarPosition = new Vector2(-camera.Position.X - (camera.View.Width / 2 - margin),
                                               -camera.Position.Y - (camera.View.Height / 2 - margin));
            specialPosition = new Vector2(-camera.Position.X - (camera.View.Width  / 2 - margin),
                                          -camera.Position.Y +  camera.View.Height / 2 - 42);  // 42 = texture.height + margin
            specialAmmoPosition = new Vector2(specialPosition.X + 35, specialPosition.Y + 5);

#if DEBUG
            if (displayDebug)
            {
                playerSpeedVector = new Vector2(-camera.Position.X, -camera.Position.Y + 330);
                playerPositionVector = new Vector2(-camera.Position.X, -camera.Position.Y + 300);
                numberOfBulletsVector = new Vector2(-camera.Position.X, -camera.Position.Y + 270);
            }
#endif
        }
    }
}
