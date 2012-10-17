﻿using System;
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
        private Matrix transform;
        private Vector2 cameraOffset;
        private Vector2 center;
        private Viewport view;
        float zoom = 1.0f;

        //Constructor updates the view
        public Camera(Viewport view)
        {
            this.view = view;
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

        public void update(Vector2 objectToFollow)
        {
            this.center = objectToFollow<

            this.transform = Matrix.CreateTranslation(new Vector3(-this.center.X, -this.center.Y, 0)) *
                Matrix.CreateScale(new Vector3(this.zoom, this.zoom, 0))*
                Matrix.CreateTranslation(new Vector3(this.view.Width / 2, this.view.Height / 2, 0));
        }

        public void getZoomInput()
        {

        }
    }
}

