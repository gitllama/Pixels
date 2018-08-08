using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Extensions
{

    public struct OrderOptions
    {
        (int left, int top, int width, int height) plane;
        (int x, int y, int width, int height) bayer;

        public int left;
        public int top;
        public int width;
        public int height;
        public int incX;
        public int incY;


        public OrderOptions((int left, int top, int width, int height) plane, (int x, int y, int width, int height) bayer)
        {
            this.plane = plane;
            this.bayer = bayer;

            left = plane.left + bayer.x;
            top = plane.top + bayer.y;
            width = plane.width - bayer.x;
            height = plane.height - bayer.y;
            incX = bayer.width;
            incY = bayer.height;
        }
    }

}

