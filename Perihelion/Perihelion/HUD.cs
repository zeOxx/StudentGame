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
        // THESE ARE ZERO BECAUSE THE CAMERA IS NOT IN PLACE YET.
        Vector2 healthAuxBarPosition;
        Vector2 specialPosition;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public HUD(ContentHolder contentHolder, Vector2 camCenter)
        {
            updateHudPositions(camCenter);

            healthAuxBar = new Models.Interface(contentHolder.healthAuxBar, healthAuxBarPosition.X, healthAuxBarPosition.Y, new Vector2(0, 0));
            special = new Models.Interface(contentHolder.special, specialPosition.X, specialPosition.Y, new Vector2(0, 0));
        }

        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        public void update()
        {
            healthAuxBar.setPosition(healthAuxBarPosition.X, healthAuxBarPosition.Y);
            special.setPosition(specialPosition.X, specialPosition.Y);
            // Calls update to all the objects to check health etc for the player. Yet to be implemented
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            healthAuxBar.Draw(spriteBatch);
            special.Draw(spriteBatch);
        }

        public void updateHudPositions(Vector2 camCenter)
        {
            healthAuxBarPosition = new Vector2(camCenter.X + 10, camCenter.Y + 10);
            specialPosition = new Vector2(camCenter.X + 10, camCenter.Y + 646);
        }
    }
}
