using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class ParticleSpawner
    {
        // Variables to be passed to particle objects
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private int life;

        private int numberOfParticles;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public ParticleSpawner(Texture2D texture, Vector2 position, int life, int numberOfParticles)
        {
            Texture = texture;
            Position = position;
            Life = life;
            NumberOfParticles = numberOfParticles;
        }

        /************************************************************************/
        /* Accessors                                                            */
        /************************************************************************/
        public Texture2D Texture
        {
            get { return this.texture; }
            private set { this.texture = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            private set { this.position = value; }
        }

        public Vector2 Velocity
        {
            get { return this.velocity; }
            private set { this.velocity = value; }
        }

        public int Life
        {
            get { return this.life; }
            private set { this.life = value; }
        }

        public int NumberOfParticles
        {
            get { return this.numberOfParticles; }
            private set { this.numberOfParticles = value; }
        }
    }
}
