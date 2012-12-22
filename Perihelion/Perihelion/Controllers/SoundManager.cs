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
            switch (soundName)
            {
                case "pang":
                    soundContent.pang.Play(0.1f, 0f, 0f);
                    break;
            }
        }

        public void playSoundWithPositioning(String soundName, Models.Player player, Models.GameObject explodingObject)
        {
            int halfWidthOfScreen = 1280 / 2;
            float relativeHorizontalPosition = player.Position.X - explodingObject.Position.X;
            float panning = - (relativeHorizontalPosition / halfWidthOfScreen);


            //In case outside of view
            if (panning > 1)
            {
                panning = 1;
            }
            else if (panning < 0)
            {
                panning = 0;
            }

            switch(soundName)
            {
                case "pang":
                    soundContent.pang.Play(0.5f, 0f, panning);
                    break;
                case "explosion":
                    soundContent.explosion.Play(1f, 0f, panning);
                    break;
            }
        }

        public void playSoundtrack()
        {
            Microsoft.Xna.Framework.Media.Song instance = soundContent.soundtrack;
        }
        
    }
}
