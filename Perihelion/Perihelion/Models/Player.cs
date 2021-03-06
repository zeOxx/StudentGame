﻿using System;
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
        
        // Powers and stuff
        private float wellMultiplier;
        private float wellStatus;
        private int auxiliaryPower;

        /************************************************************************/
        /*  Constructors for Player object                                      */
        /************************************************************************/
        public Player()
            : base()
        {
            WellMultiplier = 1;
            WellStatus = 0;
            AuxPower = 100;
        }

        public Player(Texture2D texture_ship,   Texture2D texture_turret, 
                      Texture2D texture_bullet, Texture2D texture_rocket,
                      float x, float y, Vector2 velocity, int health)
            : base(texture_ship, x, y, velocity, health)
        {
            TurretTexture = texture_turret;
            BulletTexture = texture_bullet;
            RocketTexture = texture_rocket;
            WellMultiplier = 1;
            WellStatus = 0;
            AuxPower = 100;
            Bullets = true;
            Rockets = true;
            // Temp
            Speed = 2;
            MaxSpeed = 5;
            RocketAmmo = 5;

            ShootingBullets = false;
            ShootingRockets = false;

            bullets = new List<Projectile>();
        }

        public Player(Texture2D texture, Texture2D texture_turret, Texture2D texture_bullet, float x, float y, Vector2 velocity, int health, float damageMultiplier, float attackMultiplier, float wellMultiplier, int wellStatus, int auxiliaryPower)
            : base(texture, x, y, velocity, health, damageMultiplier, attackMultiplier)
        {
            TurretTexture = texture_turret;
            WellMultiplier = wellMultiplier;
            WellStatus = wellStatus;
            AuxPower = auxiliaryPower;
        }
        /************************************************************************/
        /*  Get/set functions for Player attributes                             */
        /************************************************************************/
        protected float WellMultiplier
        {
            get { return this.wellMultiplier; }
            set { this.wellMultiplier = value; }
        }

        public float WellStatus
        {
            get { return this.wellStatus; }
            set { this.wellStatus = value; }
        }

        protected int AuxPower
        {
            get { return this.auxiliaryPower; }
            set { this.auxiliaryPower = value; }
        }

        /************************************************************************/
        /*  Update functions for Player attributes                              */
        /************************************************************************/
        public void update(Vector2 velocity, Vector2 rightStick, GameTime gameTime)
        {

            base.unitUpdate(velocity, rightStick, gameTime);
        }

        public void updateWellMultiplier(float i)
        {
            this.wellMultiplier += i;
        }

        public void updateAuxiliaryPower(int i)
        {
            this.auxiliaryPower += i;
        }

        public override void Draw(SpriteBatch spriteBatch, bool debug)
        {
            base.Draw(spriteBatch, debug);

            spriteBatch.Draw(texture_turret,
                position,
                null,
                Color.White,
                (float)turretRotationAngle,
                origin,
                1.0f,
                SpriteEffects.None,
                0f);

            // Draw each projectile in the list
            foreach (Models.Projectile projectiles in bullets)
            {
                projectiles.Draw(spriteBatch, debug);
            }
        }

        public void stop()
        {
            speed = 0;
        }
    }
}
