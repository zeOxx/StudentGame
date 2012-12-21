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
using System.Threading;
using Perihelion.Models;
using Perihelion.Controllers;

namespace Perihelion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Gameworld gameWorld;
        Controller gameController;
        ContentHolder contentHolder;
        InputHandler inputHandler;
        SoundManager soundManager;
        Highscores highScores;

        Thread thread;

        // Handling gamestates
        public enum GameStates
        {
            Menu,
            Running
        }

        public static GameStates gamestate;

        private List<spawnEnemies> enemies = new List<spawnEnemies>();

        float spawn = 0.0f;
        Random random = new Random();

        // Window properties
        private int height = 720;
        private int width = 1280;
        private string gameName = "Perihelion";

        // FPS and UPS
        private int updates;
        private int frames;
        private float elapsed;
        private float totalElapsed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = height;
            graphics.PreferredBackBufferWidth = width;

            Window.Title = gameName;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gamestate = GameStates.Menu;
            highScores = new Highscores();
            contentHolder = new ContentHolder(this.Content);
            soundManager = new SoundManager(contentHolder);
            gameController = new Controllers.Controller(contentHolder, soundManager, highScores, gameName, width, height);
            inputHandler = new InputHandler();
            gameWorld = new Models.Gameworld(contentHolder, GraphicsDevice.Viewport, 4096);    //TODO SINGLETON

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Update(GameTime gameTime)
        {
            spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (spawnEnemies enemy in enemies)
                enemy.update(graphics.GraphicsDevice);

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Stores the keyboardstate in a variable
            KeyboardState keyboard = Keyboard.GetState();

            // Exits the game when ESC is pressed
            if ((gamestate == GameStates.Running && keyboard.IsKeyDown(Keys.Escape))
                || gameController.menuHandler.mainMenu.Exiting)
            {
                thread = new Thread(() => highScores.sendScore("1", "Inge", "yau", 180)); 
                thread.Start();
                
                Exit();
            }
            
            // Checks to see what should be updated, menu or gameworld
            if (gamestate == GameStates.Menu)
            {
                gameController.updateMenu(inputHandler, gameTime, highScores);
            }
            else if (gamestate == GameStates.Running)
            {
                // Sends gamestate to controller and receives updated state. 
                gameController.updateGameWorld(ref gameWorld, gameTime, inputHandler);
                //loadEnemies();
            }

            // Calculates Frames Per Second and Updates Per Second and puts them in the window title
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            totalElapsed += elapsed;

            if (totalElapsed > 1)
            {
                Window.Title = gameName + " | " + frames + "FPS " + updates + "UPS";
                totalElapsed = 0;
                updates = 0;
                frames = 0;
            }

            // TODO: Add your update logic here
            base.Update(gameTime);
            updates++;

            if (gameController.menuHandler.Active)
            {
                if (gamestate != GameStates.Menu)
                    gamestate = GameStates.Menu;
            }
            else
            {
                gamestate = GameStates.Running;
            }
        }

        //public void loadEnemies()
        //{
        //    int randY = random.Next(100, 400);

        //    if (spawn >= 1)
        //    {
        //        spawn = 0.0f;
        //        if (enemies.Count() < 4)
        //            enemies.Add(new spawnEnemies(Content.Load<Texture2D>("texturePlayer"), new Vector2(1100, randY)));

        //    }

        //    for (int i = 0; i < enemies.Count(); i++)
        //    {
        //        if (!enemies[i].isVisible)
        //        {
        //            enemies.RemoveAt(i);
        //            i--;
        //        }
        //    }
        //}


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if (gamestate == GameStates.Menu)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                gameController.menuHandler.Draw(spriteBatch);
            }
            else if (gamestate == GameStates.Running)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, gameWorld.getCamera().Transform);
                gameWorld.Draw(spriteBatch);
                foreach (spawnEnemies enemy in enemies)
                enemy.draw(spriteBatch);
            } 
            spriteBatch.End();
            base.Draw(gameTime);

            frames++;
        }
    }
}
