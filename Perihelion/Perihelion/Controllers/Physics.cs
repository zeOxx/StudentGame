using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Perihelion.Models;

namespace Perihelion.Controllers
{
    class Physics
    {
        Physics()
        {

        }

        /************************************************************************/
        /* Detects collisions between for a GameObject on other objects         */
        /************************************************************************/
        public bool checkCollision(GameObject go1, GameObject[] go2)
        {
            int i;
            for (i = 0; i < go2.Length; i++)
            {
                if (go1.BoundingBox.Intersects(go2[i].BoundingBox))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
