﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Perihelion
{
    class ContentHolder : Microsoft.Xna.Framework.Game
    {
        // Player textures
        public Texture2D texturePlayer;
        public Texture2D texturePlayerTurret;
        public Texture2D texturePlayerBullet;
        public Texture2D texturePlayerSpecal01;

        // Object textures
        public Texture2D textureRock01;
        public Texture2D textureRock02;

<<<<<<< HEAD
        // Enemy textures
        public Texture2D textureEnemy;
=======
        // Menu textures
        public Texture2D title;
>>>>>>> ea9caf17b3f0846ed19c3f7b85f7c51b7b9a1156

        // Background textures
        public Texture2D[] bg_variant0 = new Texture2D[2];
        public Texture2D[] bg_variant1 = new Texture2D[2];

        // Particles
        public Texture2D particle_test;

        // HUD elements
        public Texture2D healthAuxBar;
        public Texture2D special;

        //Sound effects
        public SoundEffect playerShootingGun;
        public SoundEffect playerGunWindDown;
        public SoundEffect pang;
        public Song soundtrack;

        // Fonts
        public SpriteFont menuFont;

#if DEBUG
        // Debug font
        public SpriteFont debugFont;
#endif

        public ContentHolder(ContentManager content)
        {
            loadTextures(content);
            loadSounds(content);
            loadParticles(content);
            loadFonts(content);
        }

        private void loadSounds(ContentManager content)
        {
            playerShootingGun = content.Load<SoundEffect>("SoundEffects\\pang");
            playerGunWindDown = content.Load<SoundEffect>("SoundEffects\\s_gun_wind_down");
            pang = content.Load<SoundEffect>("SoundEffects\\pang");
        }

        private void loadTextures(ContentManager content)
        {
            // Player
            texturePlayer = content.Load<Texture2D>("PlayerTextures\\ship_bare");
            texturePlayerTurret = content.Load<Texture2D>("PlayerTextures\\ship_turret");
            texturePlayerBullet = content.Load<Texture2D>("PlayerTextures\\bullet");
            texturePlayerSpecal01 = content.Load<Texture2D>("PlayerTextures\\weapon_special_01");

            // Objects
            textureRock01 = content.Load<Texture2D>("Environment\\rock_01");
            textureRock02 = content.Load<Texture2D>("Environment\\rock_02");

            // Menu
            title = content.Load<Texture2D>("Menu\\Title");

            //Background arrays
            bg_variant0[0] = content.Load<Texture2D>("Backgrounds\\bg_01_0");
            bg_variant0[1] = content.Load<Texture2D>("Backgrounds\\bg_01_1");

            bg_variant1[0] = content.Load<Texture2D>("Backgrounds\\bg_02_0");
            bg_variant1[1] = content.Load<Texture2D>("Backgrounds\\bg_02_1");

            // HUD
            healthAuxBar = content.Load<Texture2D>("HUD\\hud_healthAux");
            special = content.Load<Texture2D>("HUD\\hud_special_01");
        }

        private void loadParticles(ContentManager content)
        {
            particle_test = content.Load<Texture2D>("Particles\\particle_test");
        }

        private void loadFonts(ContentManager content)
        {
            menuFont = content.Load<SpriteFont>("menuFont");

#if DEBUG
            // Debug font
            debugFont = content.Load<SpriteFont>("debugFont");
#endif
        }
    }
}
