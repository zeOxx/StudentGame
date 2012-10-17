using System;
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

        // Object textures
        public Texture2D textureRock01;
        public Texture2D textureRock02;

        // HUD elements
        public Texture2D healthAuxBar;
        public Texture2D special;

        public ContentHolder(ContentManager content)
        {   
            // Player
            texturePlayer = content.Load<Texture2D>("ship_bare");
            texturePlayerTurret = content.Load<Texture2D>("ship_turret");

            // Objects
            textureRock01 = content.Load<Texture2D>("rock_01");
            textureRock02 = content.Load<Texture2D>("rock_02");

            // HUD
            healthAuxBar = content.Load<Texture2D>("hud_healthAux");
            special = content.Load<Texture2D>("hud_special_01");
        }
    }
}
