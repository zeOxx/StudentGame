using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Perihelion.Models
{
    class ParticleEmitter
    {
        // Variables to be passed to particle objects
        private Texture2D texture;
        private Vector2 position;
        private Vector2 velocity;
        private int life;

        private Random random;

        private int lifespan;                       // Stores the lifespan of the emitter (in ms). A lifespan of 0 makes the particleeffect last forever, and means it's attached to an object
        private int lifeTimer;                      // Stores the time that has passed.
        private int timeBetweenParticles;           // Stores the ideal time between particles spawning.
        private int timeBetweenUpdates;             // Stores the time that has passed between updates.
        private bool isActive;                      // Does a check to see if the emitter is active or not
        private bool randomDirections;

        List<Particle> particles;                   // List of particles to be emitted.

        /************************************************************************/
        /* Constructor                                                          */
        /************************************************************************/
        public ParticleEmitter(Texture2D texture, Vector2 position, int lifespan, int life, int timeBetweenParticles, bool randomDirections, Vector2 direction)
        {
            particles = new List<Particle>();

            random = new Random();

            Texture = texture;
            Position = position;
            Velocity = direction;
            Lifespan = lifespan;
            Life = life;
            TimeBetweenParticles = timeBetweenParticles;
            RandomDirections = randomDirections;

            IsActive = true;

            TimeBetweenParticles = 100;
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
        
        public int Lifespan
        {
            get { return this.lifespan; }
            private set { this.lifespan = value; }
        }

        public int Life
        {
            get { return this.life; }
            private set { this.life = value; }
        }

        public bool IsActive
        {
            get { return this.isActive; }
            private set { this.isActive = value; }
        }

        public bool RandomDirections
        {
            get { return this.randomDirections; }
            private set { this.randomDirections = value; }
        }

        public int LifeTimer
        {
            get { return this.lifeTimer; }
            private set { this.lifeTimer = value; }
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
        public void update(GameTime gameTime, ContentHolder content, Vector2 position, Vector2 velocity)
        {
            LifeTimer += gameTime.ElapsedGameTime.Milliseconds;
            TimeBetweenUpdates += gameTime.ElapsedGameTime.Milliseconds;

            if (lifespan == 0)
                Position = position;

            float randomNumberX;
            float randomNumberY;
            
            if (lifespan > LifeTimer || lifespan == 0)
            {
                if (TimeBetweenUpdates > TimeBetweenParticles)
                {
                    Vector2 prevVelocity = Velocity;

                    if (RandomDirections)
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
                            randomNumberX = random.Next(1, 3);
                            if (randomNumberX == 1)
                                velocity.X += 0.2f;
                            else if (randomNumberX == 2)
                                velocity.X -= 0.2f;
                        }

                        if (Velocity.Y != 0)
                        {
                            randomNumberY = random.Next(1, 3);
                            if (randomNumberY == 1)
                                velocity.Y += 0.2f;
                            else if (randomNumberY == 2)
                                velocity.Y -= 0.2f;
                        }
                    }

                    Particle tempParticle = new Particle(Texture, Position, Velocity, Life, 0.0f, 1.0f);

                    particles.Add(tempParticle);

                    TimeBetweenUpdates = 0;

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
            {
                IsActive = false;
            }
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
