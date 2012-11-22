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

		int levelSize = 4096;

		private Player playerObject;
		private List<Collidable> rocks = new List<Collidable>();
		private Background background01;
		private Background background02;
		private Camera camera;
		private spawnEnemies enemy;
		private HUD hud;
		private Rectangle levelBounds;
		private Controllers.ParticleSystem particleSystem;
		private List<Enemy> enemies = new List<Enemy>();
		private Controllers.UnitHandler unithandler = new Controllers.UnitHandler(); //WIP - mostly empty

		public Gameworld(ContentHolder contentHolder, Viewport view, int levelSize)
		{
			particleSystem = new Controllers.ParticleSystem();
			initializeGameworld(contentHolder);
			camera = new Camera(view);
			hud = new HUD(contentHolder, camera);
			LevelSize = levelSize;
		}

        public Controllers.ParticleSystem getParticleSystem()
        {
            return particleSystem;
        }

		public Player getPlayer()
		{
			return playerObject;
		}

		public void setPlayer(Player updatedPlayerObject)
		{
			playerObject = updatedPlayerObject;
		}

		public Player PlayerObject
		{
			get { return playerObject; }
			set { this.playerObject = value; }
		}

		public int LevelSize
		{
			get { return this.levelSize; }
			set { this.levelSize = value; }
		}

		public Rectangle LevelBounds
		{
			get { return this.levelBounds; }
			set { this.levelBounds = value; }
		}

		public int getNumberOfCurrentCollidables()
		{
			return numbOfCurrentCollidables;
		}

#if DEBUG
		public void setDebug()
		{
			if (hud.DisplayDebug)
				hud.DisplayDebug = false;
			else
				hud.DisplayDebug = true;
		}
#endif

		private void initializeGameworld(ContentHolder contentHolder)
		{
			playerObject = new Player(contentHolder.texturePlayer, contentHolder.texturePlayerTurret, contentHolder.texturePlayerBullet, 0, 0, Vector2.Zero, 100);
			levelBounds = createBounds();
			background01 = new Background(contentHolder.bg_variant0, playerObject.Velocity, levelBounds, 0, 0, 0.4f);
			background02 = new Background(contentHolder.bg_variant1, playerObject.Velocity, levelBounds, 0, 0, 0.2f);

			// The following should (and will, eventually) be loaded by reading a file generated by an editor,
			//  so this is temp, testing etc

			// NOT THAT THE RED PARTICLE ATTACHED TO THE SHIP IS OBVIOUSLY PLACEHOLDER

			particleSystem.newEmitter(contentHolder.particle_smoke, getPlayer().Position, 0, 200, 12, false, getPlayer().Velocity);
			particleSystem.newSpawner(contentHolder.particle_test, new Vector2(-100, -100), 1000, 500, 3, true, new Vector2(0, 0));

			rocks.Add(new Collidable(contentHolder.textureRock01, 150, 300, Vector2.Zero, true, 400));
			rocks.Add(new Collidable(contentHolder.textureRock02, -250, -330, Vector2.Zero, true, 40));
			rocks.Add(new Collidable(contentHolder.textureRock01, 500, 300, Vector2.Zero, true, 70));
			rocks.Add(new Collidable(contentHolder.textureRock02, -100, 250, Vector2.Zero, true, 200));

            for (int i = 0; i < 20; i++)
            {

                enemies.Add(new Enemy(contentHolder.texturePlayer, contentHolder.texturePlayerTurret, contentHolder.texturePlayerBullet, i*20 + 200, i*20 + 200, Vector2.Zero, 100));
            }
		}

		// Creates the bounds for the level
		//  KEEP IN MIND THAT THE SIZE HAS TO ADHERE TO THE POWER OF TWO!
		//  THAT MEANS 8,16,32,64,128 etc etc
		private Rectangle createBounds()
		{
			Rectangle tempRect = new Rectangle(-(LevelSize / 2), -(LevelSize / 2), LevelSize, LevelSize);
			return tempRect;
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			background01.Draw(spriteBatch);
			background02.Draw(spriteBatch);

			for (int i = 0; i < rocks.Count; i++)
			{
				rocks[i].Draw(spriteBatch);
			}

			foreach (Enemy enemy in enemies)
			{
				enemy.Draw(spriteBatch);
			}

			particleSystem.Draw(spriteBatch);
			playerObject.Draw(spriteBatch);
			hud.Draw(spriteBatch);
		}

		public void update(GameTime gameTime, ContentHolder contentHolder)
		{
			camera.update(playerObject.Position);
			background01.update(playerObject.Velocity);
			background02.update(playerObject.Velocity);
			hud.updateHudPositions(camera);
			hud.update(playerObject);
			particleSystem.update(gameTime, contentHolder, getPlayer().Position, getPlayer().Velocity);
			
		}

		//Returns camera to draw function
		public Camera getCamera()
		{
			return camera;
		}

		public List<Collidable> getRock()
		{
			return rocks;
		}

		public List<Enemy> EnemyList
		{
			get { return enemies; }
		}

		public void updateEnemies(GameTime gameTime)
		{
			foreach (Enemy enemy in enemies)
			{
				Vector2 stick = unithandler.getEnemyStickVector(PlayerObject, enemy);
				int shot = unithandler.getEnemyTarget(PlayerObject, enemy);
				bool rocket = false;
				if (shot == 0)
				{
					enemy.update(Vector2.Zero, stick, gameTime, rocket);
				}
				else if (shot == 1)
				{
					enemy.update(stick, stick, gameTime, rocket);
				}
				else
				{
					enemy.update(Vector2.Zero, Vector2.Zero, gameTime, rocket);
				}
			}
		}
	}

	
}
