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
        private bool randomDirection;
        private bool isActive;

        private Random random;

        private int numberOfParticles;
        private int currentNumberOfParticles;
        private int timeBetweenParticles;           // Stores the ideal time between particles spawning.
        private int timeBetweenUpdates;             // Stores the time that has passed between updates.

        List<Particle> particles;

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public ParticleSpawner(Texture2D texture, Vector2 position, int life, int timeBetweenParticles, int numberOfParticles, bool randomDirection, Vector2 direction)
        {
            particles = new List<Particle>();

            random = new Random();

            Texture = texture;
            Position = position;
            Velocity = direction;
            Life = life;
            NumberOfParticles = numberOfParticles;
            RandomDirection = randomDirection;

            IsActive = true;
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

        public int CurrentNumberOfParticles
        {
            get { return this.currentNumberOfParticles; }
            private set { this.currentNumberOfParticles = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            private set { this.isActive = value; }
        }

        public bool RandomDirection
        {
            get { return this.randomDirection; }
            private set { this.randomDirection = value; }
        }

        public int TimeBetweenParticles
        {
            get { return this.timeBetweenParticles; }
            private set { this.timeBetweenParticles = value; }
        }

        public int TimeBetweenUpdates
        {
            get { return this.timeBetweenUpdates; }
            private set { this.timeBetweenUpdates = value; }
        }
        /************************************************************************/
        /* Methods                                                              */
        /************************************************************************/
        // This method is copied from the following website: 
        //  http://stackoverflow.com/questions/3365337/best-way-to-generate-a-random-float-in-c-sharp
        //  and has been slightly altered to fit in. 
        static float NextFloat(Random random)
        {
            double mantissa = (random.NextDouble() * 2.0) - 1.0;
            double exponent = Math.Pow(2.0, random.Next(-2, 2));
            return (float)(mantissa * exponent);
        }

        /************************************************************************/
        /* XNA Methods                                                          */
        /************************************************************************/
        public void update(GameTime gameTime)
        {
            TimeBetweenUpdates += gameTime.ElapsedGameTime.Milliseconds;

            float randomNumberX;
            float randomNumberY;

            // Adds the correct amount of particles to the list
            if (currentNumberOfParticles < NumberOfParticles)
            {
                if (TimeBetweenUpdates > TimeBetweenParticles)
                {
                    Vector2 prevVelocity = Velocity;

                    if (RandomDirection)
                    {
                        randomNumberX = NextFloat(random);
                        randomNumberY = NextFloat(random);

                        velocity.X = randomNumberX;
                        velocity.Y = randomNumberY;
                    }
                    else
                    {
                        if (Velocity.X != 0)
                        {
                            randomNumberX = (float)random.NextDouble();
                            if (randomNumberX < 0.5f)
                                velocity.X += (float)random.NextDouble();
                            else if (randomNumberX >= 0.5f)
                                velocity.X -= (float)random.NextDouble();
                        }

                        if (Velocity.Y != 0)
                        {
                            randomNumberY = (float)random.NextDouble();
                            if (randomNumberY < 0.5f)
                                velocity.Y += (float)random.NextDouble();
                            else if (randomNumberY >= 0.5f)
                                velocity.Y -= (float)random.NextDouble();
                        }
                    }

                    Particle tempParticle = new Particle(Texture, Position, Velocity, Life, 1.0f, true);

                    particles.Add(tempParticle);

                    CurrentNumberOfParticles++;

                    TimeBetweenUpdates++;

                    Velocity = prevVelocity;
                }
            }

            for (int i = 0; i < particles.Count(); i++)
            {
                particles[i].update(gameTime);

                if (particles[i].TotalLife > particles[i].Life)
                    particles.RemoveAt(i);
            }

            if (particles == null)
                IsActive = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particles.Count(); i++)
            {
                if (particles[i].TotalLife > particles[i].StartFading)
                {
                    particles[i].fade();
                }

                particles[i].Draw(spriteBatch);
            }
        }
    }
}
