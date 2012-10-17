﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Perihelion;

namespace Perihelion.Models
{
    class Gameworld : Microsoft.Xna.Framework.Game        // TODO SINGLETON
    {
        // the max int will be used when making levels in the editor, so it's unused at the moment
        const int numOfMaxCollidables = 10;
        // current will also be used in the editor eventually, but for now it's set to const and
        //  is used as a max for testing purposes
        const int numbOfCurrentCollidables = 5;

        private Player playerObject;
        private Collidable[] rock = new Collidable[numbOfCurrentCollidables];
        private Camera camera;
        private HUD hud;
        //private Projectile[] playerBullets;

        public Gameworld(ContentHolder contentHolder, Viewport view)
        {
            initializeGameworld(contentHolder);
            camera = new Camera(view);
            hud = new HUD(contentHolder, camera.Center);
        }

        public Player getPlayer()
        {
            return playerObject;
        }

        public void setPlayer(Player updatedPlayerObject)
        {
            playerObject = updatedPlayerObject;
        }

        private void initializeGameworld(ContentHolder contentHolder)
        {
            playerObject = new Player(contentHolder.texturePlayer, contentHolder.texturePlayerTurret, 100, 100, Vector2.Zero, 100, 100);

            // The following should (and will, eventually) be loaded by reading a file generated by an editor,
            //  so this is temp, testing etc

            rock[0] = new Collidable(contentHolder.textureRock01, 150, 300, Vector2.Zero);
            rock[1] = new Collidable(contentHolder.textureRock02, -250, -330, Vector2.Zero);
            rock[2] = new Collidable(contentHolder.textureRock01, 500, 300, Vector2.Zero);
            rock[3] = new Collidable(contentHolder.textureRock02, -100, 250, Vector2.Zero);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerObject.Draw(spriteBatch);
            hud.Draw(spriteBatch);
            rock[0].Draw(spriteBatch);
            rock[1].Draw(spriteBatch);
            rock[2].Draw(spriteBatch);
            rock[3].Draw(spriteBatch);
        }

        public void update()
        {
            camera.update(playerObject.getPosition());
            hud.updateHudPositions(camera.Center);
            hud.update();
        }

        //Returns camera to draw function
        public Camera getCamera()
        {
            return camera;
        }
    }
}
