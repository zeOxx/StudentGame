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
    public class cameraTest : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spriteTexture;
        public Rectangle spriteRectangle;

        Vector2 spriteOrigin;

        public Vector2 spritePosition;
        float rotation;

        Vector2 spriteVelocity;
        const float TANGENTIAL_VELOCITY = 5.0f;
        float friction = 0.1f;

        Camera camera;

        Texture2D backgroundTexture;
        Vector2 backgroundPosition;

        public cameraTest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
                
        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            this.spriteTexture = Content.Load<Texture2D>("texturePlayer");
            this.spritePosition = new Vector2(300, 250);

            this.backgroundTexture = Content.Load<Texture2D>("background");

            camera = new Camera(GraphicsDevice.Viewport);
        }


        //Handles Keypresses and udates the camera-class accordingly
        protected override void Update(GameTime gameTime)
        {
            this.spriteRectangle = new Rectangle((int)this.spritePosition.X, (int)this.spritePosition.Y,
                this.spriteTexture.Width, this.spriteTexture.Height);
            
            this.spritePosition = this.spriteVelocity + this.spritePosition;
            this.spriteOrigin = new Vector2(this.spriteRectangle.Width / 2, this.spriteRectangle.Height / 2);

            if (Keyboard.GetState().IsKeyDown(Keys.X)) camera.Zoom += 0.01f;
            if (Keyboard.GetState().IsKeyDown(Keys.Z)) camera.Zoom -= 0.01f;
            
            if (Keyboard.GetState().IsKeyDown(Keys.Right))   this.rotation += 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) this.rotation -= 0.1f;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                this.spriteVelocity.X = (float)Math.Cos(this.rotation) * TANGENTIAL_VELOCITY;
                this.spriteVelocity.Y = (float)Math.Sin(this.rotation) * TANGENTIAL_VELOCITY;
            }
            else if (this.spriteVelocity != Vector2.Zero)
            {
                float i = this.spriteVelocity.X;
                float j = this.spriteVelocity.Y;

                this.spriteVelocity.X = i -= this.friction * i;
                this.spriteVelocity.Y = j -= this.friction * j;
            }

            camera.update(gameTime, this);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);   //Backgroundcolor if you move outside the background-picture

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            spriteBatch.Draw(this.backgroundTexture, this.backgroundPosition, Color.White); //Draw Background
            spriteBatch.Draw(this.spriteTexture, this.spritePosition, null, Color.White, this.rotation, //Draw the player
                this.spriteOrigin, 1f, SpriteEffects.None, 0.0f);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
