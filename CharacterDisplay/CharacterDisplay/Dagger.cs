using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharacterDisplay
{
    class Dagger
    {
        public int DaggerCoordsX{ get; set;}
        public int DaggerCoordsY{ get; set;}
        public int DaggerSpeed{ get; set;}
        public Texture2D DaggerTexture2D { get; set;}
        public Rectangle DaggerRect { get; set; }
        public Boolean DaggerHit { get; set; }
        public Boolean PointsGiven { get; set; }

        /// <summary>
        /// Store postion, speed and image of dagger
        /// </summary>
        /// <param name="daggerCoordsX"></param>
        /// <param name="daggerCoordsY"></param>
        /// <param name="daggerSpeed"></param>
        /// <param name="daggerTexture2D"></param>
        /// <param name="daggerRect"></param>
        public Dagger(int daggerCoordsX, int daggerCoordsY, int daggerSpeed, Texture2D daggerTexture2D, Rectangle daggerRect, Boolean daggerHit = false, Boolean pointsGiven = false){

            DaggerCoordsX = daggerCoordsX;
            DaggerCoordsY = daggerCoordsY;
            DaggerSpeed = daggerSpeed;
            DaggerTexture2D = daggerTexture2D;
            DaggerRect = daggerRect;
            DaggerHit = daggerHit;
            PointsGiven = pointsGiven;

        }

        /*
        public Dagger(int daggerCoordsX, int daggerCoordsY, int daggerSpeed)
        {

            DaggerCoordsX = daggerCoordsX;
            DaggerCoordsY = daggerCoordsY;
            DaggerSpeed = daggerSpeed;

        }
         * */
    }
}
