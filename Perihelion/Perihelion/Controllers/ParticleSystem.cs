using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Controllers
{
    class ParticleSystem
    {
        private List<Models.ParticleEmitter> emitterList;
        //private List<Models.ParticleSpawner> spawnerList;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public ParticleSystem()
        {
            // Initiallizes the base system. Use the methods for creating particle effects
            emitterList = new List<Models.ParticleEmitter>();
        }

        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        // The emitter will keep spawning particles for a set amount of time. If you want to spawn just a couple
        //  (say, from a bullethit), use the spawn mehtod.
        public void newEmitter(Texture2D texture, Vector2 position, int lifespan, int life)
        {
            Models.ParticleEmitter tempEmitter = new Models.ParticleEmitter(texture, position, lifespan, life);

            emitterList.Add(tempEmitter);
        }

        // The spawn method is used when you want to spawn a couple of particles for a brief time (eg. collision events)
        public void spawn(Texture2D texture, Vector2 position, Vector2 velocity)
        {
            /*
             * Models.ParticleSpawner tempSpawner = new Models.ParticleSpawner(texture, position, velocity, life);
             * 
             * spawnerList.Add(tempSpawner);
             */
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(GameTime gameTime, ContentHolder content)
        {
            if (emitterList != null)
            {
                for (int i = 0; i < emitterList.Count(); i++)
                {
                    if (emitterList[i].IsActive)
                        emitterList[i].update(gameTime, content);
                    else
                        emitterList.RemoveAt(i);
                }
            }

            /*
             * if (spawnerList != null)
             * {
             *      for (int i = 0; i < spawnerList.Count(); i++)
             *      {
             *          spawnerList[i].update(); 
             *      }
             * }
             */
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (emitterList != null)
            {
                for (int i = 0; i < emitterList.Count(); i++)
                {
                    emitterList[i].Draw(spriteBatch);
                }
            }

            /* if (emitterList != null)
             * {
             *      for (int i = 0; i < emitterList.Count(); i++)
             *      {
             *          emitterList[i].Draw(spriteBatch);
             *      }
             * }
             */
        }


    }
}
