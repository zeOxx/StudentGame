using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Perihelion
{

    class Camera
    {
        private float stiffness = 1800.0f;
        private float damping = 600.0f;
        private float mass = 50.0f;
        private Vector2 position;
        private Vector2 velocity;
        private Matrix transform;
        private Vector2 center;
        private Viewport view;
        float zoom = 0.3f;
        int timeSinceLastUpdate;
        float length;

        //Constructor updates the view
        public Camera(Viewport view)
        {
            this.view = view;
            center = new Vector2(view.Height / 2, view.Width / 2);
            timeSinceLastUpdate = 0;
        }

        public Matrix Transform
        {
            get { return this.transform; }
        }


        public float Zoom
        {
            get { return this.zoom; }
            set { this.zoom = value; }
        }

        public Vector2 Center
        {
            get { return this.center; }
        }

        public Viewport View
        {
            get { return this.view; }
        }

        public float Stiffness
        {
            get { return stiffness; }
            set { stiffness = value; }
        }

        public float Damping
        {
            get { return damping; }
            set { damping = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public float Mass
        {
            get { return mass; }
            set { mass = value; }
        }

        public void update(Models.GameObject player, GameTime gameTime)
        {
            if (gameTime == null)
            {
                throw new ArgumentNullException("gameTime");
            }
            Matrix transform = Matrix.Identity;
            transform.Forward = new Vector3(player.Velocity.X, player.Velocity.Y, 0);
            

            Vector2 desiredPositionOffset = player.Velocity * player.Speed;
            
            //Vector2 targetPoint = new Vector2();
            //targetPoint = new Vector2(( (player.Velocity.X * player.Speed)),
            //                            ( -(player.Velocity.Y * player.Speed))
            //                            ) * 10 + player.Position;

            Vector2 desiredPosition = (player.Position + player.Velocity) +
                    Vector2.TransformNormal(desiredPositionOffset, transform);

            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 stretch = position + desiredPosition ;
            Vector2 force   = - stiffness * stretch - damping * velocity;

            // Apply acceleration
            Vector2 acceleration = force / mass;
            velocity += acceleration * elapsed;

            // Apply velocity
            position += velocity * elapsed;

            //this.center = objectToFollow;

            //Vector2 deltaCameraMovement = new Vector2((( player.Velocity.X  )    ),
            //                                        (( - player.Velocity.Y  )    ));

            //Vector2 maxCameraMovement = new Vector2(((  1 * player.Speed)),
            //                                        ((  0 * player.Speed)));

            //Vector2 oldTargetPoint = new Vector2();
            
            //Vector2 vectorMax= getDaVector(player.Position, maxCameraMovement);
            //Vector2 vectorActual = getDaVector(player.Position, center);
            //double actualLength = vectorActual.Length(); 

            

            //Der spilleren er om et halvt sekund (30 frames) om den holder samme fart og retning
            //if ((gameTime.ElapsedGameTime.Ticks - timeSinceLastUpdate) > 15)
            //{
                    //if(length < 1f)
                    //{
                    //    if((targetPoint) != player.Position)
                    //    {
                    //        length += 0.01f;
                    //        Console.Out.WriteLine(length);
                    //    }
                    //    else if(length != 0)
                    //    {
                    //        Console.Out.WriteLine(length);
                    //        length -= 0.1f;
                    //    }
                    //}

                    //center = Vector2.SmoothStep(center, targetPoint, 1f) + player.Position;

                    //center.X = MathHelper.SmoothStep(player.Position.X, targetPoint.X, length); //+ player.Position.X;
                    //center.Y = MathHelper.SmoothStep(player.Position.Y, targetPoint.Y, length); //+ player.Position.Y;

                    //center.X = MathHelper.SmoothStep(oldTargetPoint.X, targetPoint.X, length);
                    //center.Y = MathHelper.SmoothStep(oldTargetPoint.Y, targetPoint.Y, length);

                    //center.X = MathHelper.SmoothStep(center.X, targetPoint.X, length);
                    //center.Y = MathHelper.SmoothStep(center.Y, targetPoint.Y, length);
                    //center.X = MathHelper.SmoothStep(center.X, player.Position.X, 0.5f);
                    //center.Y = MathHelper.SmoothStep(center.Y, player.Position.Y, 0.5f);

                    //oldTargetPoint = targetPoint;


            //this.transform = Matrix.CreateTranslation(new Vector3(-this.center.X, -this.center.Y, 0)) *
            this.transform = Matrix.CreateTranslation(new Vector3( position.X,  position.Y, 0)) *
                Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 0)) *
                Matrix.CreateTranslation(new Vector3(view.Width / 2, view.Height / 2, 0));
                
        }

        private double getVectorLength(Vector2 v)
        {
            return Math.Sqrt((v.X * v.X) +
                             (v.Y * v.Y));
        }

        private Vector2 getDaVector(Vector2 v1, Vector2 v2)
        {
            return new Vector2 (v1.X - v2.X, v1.Y - v2.Y);
        }
    }
}

