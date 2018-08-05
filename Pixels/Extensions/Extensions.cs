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

#if DEBUG
    public class TemplateClass
    {
        public static void DefaultLoop<T>(Pixel<T> src, Pixel<T> dst, string subPlane = null, string cfa = null, bool parallel = false) where T : unmanaged
        {
            Action<Pixel<T>> action = null;
            unsafe
            {
                fixed (T* pinSrc = src)
                fixed (T* pinDst = dst)
                {
                    /*<# var loop=@"*/
                    OrderOptions sts = new OrderOptions(src.GetPlane(subPlane), src.GetCFA(cfa));
                    if (parallel)

                        Parallel.For(sts.top, sts.top + (int)Math.Ceiling((double)sts.height / sts.incY), n =>
                        {
                            int y = n * sts.incY;
                            int index = y * src.Width;
                            for (var x = sts.left; x < sts.left + sts.width; x += sts.incX)
                            {
                                action(src);
                            }
                        });
                    else
                        for (var y = sts.top; y < sts.top + sts.height; y += sts.incY)
                            for (var x = sts.left; x < sts.left + sts.width; x += sts.incX)
                            {
                                action(src);
                            }
                    /*";>#*/
                }
            }
        }
    }

#endif
}

