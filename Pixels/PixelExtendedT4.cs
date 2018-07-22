using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;



namespace Pixels
{

    public static partial class PixelExtention
    {
        public static void ForEach2<T>(this Pixel<T> src, Func<T, T> func) where T : struct
        {
			for (int y = 0; y < src.Height; y++)
            {
                for (int x = 0; x < src.Width; x++)
                {
                    src[x, y] = func(src[x, y]);
                }
            }
        }
	}
}