using System;
using System.Collections.Generic;
using System.Text;

namespace Pixels.Standard.Analyze
{
    class Class1
    {
    }
}

/*


 int thr = 16;
 int bitshift = 10;


 // 同じ場所に代入したらダメ
 // 差分でなく絶対値
 switch (flag)
 {
     case "color":
         for (int y = 2; y < height - 2; y++)
             for (int x = 2; x < width - 2; x++)
             {
                 var index = x + y * width;
                 dst[index] = raw[index] >> bitshift;
             }
         return ToBitmap.Color(dst, width, height);
     case "color2":
         for (int y = 2; y < height - 2; y++)
             for (int x = 2; x < width - 2; x++)
             {
                 var index = x + y * width;
                 var top = raw[index - width * 2];
                 var left = raw[index + 2];
                 var right = raw[index - 2];
                 var bottom = raw[index + width * 2];
                 var center = raw[index];

                 if (
                     ((center - top) > thr) &&
                     ((center - left) > thr) &&
                     ((center - right) > thr) &&
                     ((center - bottom) > thr)
                     )
                 {
                     dst[index] = (int)((top + left + right + bottom) / 4) >> bitshift;
                 }
                 else
                 {
                     dst[index] = center >> bitshift;
                 }
             }
         return ToBitmap.Color(dst, width, height);
     default: return ToBitmap.Mono(raw, width, height);
 }
 */
