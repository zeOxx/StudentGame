using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Perihelion.Controllers
{
    class SoundManager
    {

        //private int timeSinceLastPlayed;

        private ContentHolder soundContent;

        public SoundManager(ContentHolder soundContent)
        {
            this.soundContent = soundContent;
        }


        public void playSound(String soundName)
        {
            switch(soundName)
            {
                case "pang":
                    soundContent.pang.Play();
                    break;
                case "explosion":
                    soundContent.explosion.Play();
                    break;
            }

            //int durationOfSound = soundContent.playerShootingGun.Duration.Milliseconds;
            //timeSinceLastPlayed += gameTime.ElapsedGameTime.Milliseconds;

            //if (timeSinceLastPlayed > durationOfSound)
            //{
            //    timeSinceLastPlayed = 0;
            //    soundContent.pang.Play();
            //    timeSinceLastPlayed = gameTime.ElapsedGameTime.Milliseconds;

            //}
        }
        
    }
}
