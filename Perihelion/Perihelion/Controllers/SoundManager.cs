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


        public void playGunSound()
        {
            soundContent.pang.Play();
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
