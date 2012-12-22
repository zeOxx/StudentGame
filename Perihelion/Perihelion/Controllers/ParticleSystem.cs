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
        private List<Models.ParticleSpawner> spawnerList;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public ParticleSystem()
        {
            // Initiallizes the base system. Use the methods for creating particle effects
            emitterList = new List<Models.ParticleEmitter>();
            spawnerList = new List<Models.ParticleSpawner>();
        }

        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        // The emitter will keep spawning particles for a set amount of time. If you want to spawn just a couple
        //  (say, from a bullethit), use the spawn mehtod.
        public void newEmitter(Texture2D texture, Vector2 position, int lifespan, int life, int timeBetweenParticles, bool randomDirection, Vector2 direction)
        {
            Models.ParticleEmitter tempEmitter = new Models.ParticleEmitter(texture, position, lifespan, life, timeBetweenParticles, randomDirection, direction);

            emitterList.Add(tempEmitter);
        }

        // The spawn method is used when you want to spawn a couple of particles for a brief time (eg. collision events)
        public void newSpawner(Texture2D texture, Vector2 position, int life, int timeBetweenParticles, int numberOfParticles, bool randomDirection, Vector2 direction)
        {
            Models.ParticleSpawner tempSpawner = new Models.ParticleSpawner(texture, position, life, timeBetweenParticles, numberOfParticles, randomDirection, direction);

            spawnerList.Add(tempSpawner);
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(GameTime gameTime, Vector2 position, Vector2 velocity)
        {
            if (emitterList != null)
            {
                for (int i = 0; i < emitterList.Count(); i++)
                {
                    if (emitterList[i].IsActive)
                        emitterList[i].update(gameTime, position, velocity);
                    else
                        emitterList.RemoveAt(i);
                }
            }


            if (spawnerList != null)
            {
                for (int i = 0; i < spawnerList.Count(); i++)
                {
                    if (spawnerList[i].IsActive)
                        spawnerList[i].update(gameTime);
                    else
                        spawnerList.RemoveAt(i);
                }
            }
             
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

            if (emitterList != null)
            {
                 for (int i = 0; i < spawnerList.Count(); i++)
                 {
                     spawnerList[i].Draw(spriteBatch);
                 }
            }
        }
    }
}
