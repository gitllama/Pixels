using Pixels.Standard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Pixels.Framework
{
    /*<# var method = File.ReadAllText(Host.ResolvePath(@"WritableBitmap.cs")) #>*/
    public static partial class WritableBitmapExtentions
    {
        /*<# var i = @"*/
        public static void ToWriteableBitmap24(this Pixel<int> src, WriteableBitmap bitmap)
        {
            bitmap.Lock();

            //PixelDeveloper.Demosaic(src.pix, bitmap.BackBuffer, bitmap.PixelWidth, bitmap.PixelHeight, bitmap.BackBufferStride);

            bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            bitmap.Unlock();
        }
        /*";#>*/
        
        /*<#= i.Replace("int", "double") #>*/

        /*<#= i.Replace("int", "float") #>*/
        
        /*T4[A]{*/
        public static void ToWriteableBitmap32(this Pixel<int> src, WriteableBitmap bitmap)
        {
        }
        /*}T4*/
        
        /*<#= M["A"].Replace("int", "double") #>*/

    }
}
