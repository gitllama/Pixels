using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using Pixels.Standard;

namespace Pixels.Framework
{
    public class PixelDevelopingOpenCV : PixelDeveloping
    {
        public UInt16[] CV_16UC1;
        public UInt16[] CV_16UC3;
        public PixelDevelopingOpenCV(int width, int height) : base(width, height)
        {
            CV_16UC1 = new UInt16[width * height];
            CV_16UC3 = new UInt16[width * height * 3];
        }
    }


    public static class PixelDevelopExtented
    {
        public static void Show(this PixelDeveloping src)
        {
            using (var matsrc = new Mat(src.Height, src.Width, MatType.CV_8UC3, src.CV_8UC3))
            {
                if (false)
                {
                    //using (var stream = new FileStream("test.bmp", FileMode.Create, FileAccess.Write))
                    //{
                    //    //16bit bitmapは壊れるやろそりゃ
                    //    var encoder = new BmpBitmapEncoder();
                    //    encoder.Frames.Add(BitmapFrame.Create(dst));
                    //    encoder.Save(stream);
                    //}
                    //using (var matdst = new Mat("test.bmp"))
                    //    Cv2.ImShow("result", matdst);
                }
                else
                {
                    Cv2.ImShow("result", matsrc);
                }
                Cv2.WaitKey(0);
                Cv2.DestroyAllWindows();
            }
        }

        public static void ToWriteableBitmap(this PixelDeveloping src, WriteableBitmap dst)
        {
            using (var matsrc = new Mat(src.Height, src.Width, MatType.CV_8UC3, src.CV_8UC3))
            {
                WriteableBitmapConverter.ToWriteableBitmap(matsrc, dst);
            }
        }




        public static void Developing<T>(this PixelDevelopingOpenCV dst, Pixel<T> src, string plane = "Full", bool parallel = false) where T : struct
        {            
            //Int32 より小さいものは前処理なしでできるが
            //それ以外はキャストが必要
            switch (src)
            {
                case Pixel<int> p:
                    DemosaicOpenCV.To16UC1(p, dst, parallel);
                    break;
                default:
                    break;
            }
            DemosaicOpenCV.Developing(dst, dst.bayer, dst.Gain.R, dst.Gain.B, parallel);
        }

        private static class DemosaicOpenCV
        {
            public unsafe static void To16UC1(Pixel<int> src, PixelDevelopingOpenCV dst, bool parallel = false)
            {
                if (parallel)
                {
                    //
                    Parallel.For(0, dst.Height, y =>
                    {
                        if ((y & 1) == 0)
                        {
                            for (int x = y * dst.Width; x < y * dst.Width + dst.Width; x++)
                            {
                                var hoge = (src.pix[x] + dst.offset) >> dst.bitshift;
                                dst.CV_16UC1[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
                            }
                        }
                        else
                        {
                            //stagger
                            for (int x = y * dst.Width + 1; x < y * dst.Width + dst.Width - 1; x++)
                            {
                                var hoge = (src.pix[x + dst.stagger.BG] + dst.offset) >> dst.bitshift;
                                dst.CV_16UC1[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
                            }
                        }
                    });
                }
                else
                {
                    if (false)
                    {
                        for (int i = 0; i < dst.Width * dst.Height; i++)
                        {
                            var hoge = src.pix[i];
                            dst.CV_16UC1[i] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
                        }
                        //stagger
                        if (false)
                        {
                            for (int y = 1; y < dst.Height; y += 2)
                            {
                                for (int x = dst.Width; x > 1; x--)
                                {
                                    var i = x + y * dst.Width;
                                    dst.CV_16UC1[i] = dst.CV_16UC1[i - 1];
                                }
                            }
                        }
                        else
                        {
                            for (int y = 1; y < dst.Height; y += 2)
                            {
                                for (int x = 0; x < dst.Width - 1; x++)
                                {
                                    var i = x + y * dst.Width;
                                    dst.CV_16UC1[i] = dst.CV_16UC1[i + 1];
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int y = 0; y < dst.Height * dst.Width; y += dst.Width * 2)
                        {
                            var span = src.pix.AsSpan().Slice(y + 0, dst.Width * 2);
                            var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, dst.Width * 2);
                            fixed (int* pin = span)
                            fixed (UInt16* pin2 = span2)
                            {
                                var p = pin;
                                var p2 = pin2;
                                var last = p + dst.Width;
                                var last2 = p + span.Length;
                                while (p < last)
                                {
                                    *p2 = (UInt16)(*p > UInt16.MaxValue ? UInt16.MaxValue : *p < UInt16.MinValue ? UInt16.MinValue : *p);
                                    p++;
                                    p2++;
                                }
                                p++;
                                while (p < last2)
                                {
                                    *p2 = (UInt16)(*p > UInt16.MaxValue ? UInt16.MaxValue : *p < UInt16.MinValue ? UInt16.MinValue : *p);
                                    p++;
                                    p2++;
                                }
                            }
                        }
                    }
                }
            }

            public static void Developing(PixelDevelopingOpenCV dst, Bayer bayer, float rgain = 1, float bgain = 1, bool parallel = false)
            {
                switch (bayer)
                {
                    case Bayer.Mono:
                        DevelopingMono(dst, parallel);
                        break;
                    case Bayer.BG:
                    case Bayer.GB:
                    case Bayer.RG:
                    case Bayer.GR:
                        DevelopingColor(dst, bayer, rgain, bgain, parallel);
                        break;
                    default:
                        break;
                }
            }

            private static void DevelopingMono(PixelDevelopingOpenCV dst, bool parallel)
            {
                if (parallel)
                {
                    //555
                    Parallel.For(0, dst.Height, y =>
                    {
                        int c = y * dst.Width * 3;
                        for (int x = y * dst.Width; x < y * dst.Width + dst.Width; x++)
                        {
                            var hoge = dst.LUT[dst.CV_16UC1[x]];
                            dst.CV_8UC3[c++] = hoge;
                            dst.CV_8UC3[c++] = hoge;
                            dst.CV_8UC3[c++] = hoge;
                        }
                    });
                }
                else
                {
                    //822
                    unsafe
                    {
                        fixed (UInt16* pin16 = dst.CV_16UC1)
                        fixed (byte* pin = dst.LUT)
                        fixed (Byte* pin8 = dst.CV_8UC3)
                        {
                            var p16 = pin16;
                            var p8 = pin8;
                            var length = pin16 + dst.CV_16UC1.Length;
                            while (p16 < length)
                            {
                                var hoge = *(pin + *p16++);
                                *p8++ = hoge;
                                *p8++ = hoge;
                                *p8++ = hoge;
                            }
                        }
                    }
                }

            }

            private static void DevelopingColor(PixelDevelopingOpenCV dst, Bayer bayer, float rgain, float bgain, bool parallel)
            {
                var gain = new float[] {
                bgain, 1, rgain,
                bgain, 1, rgain,
                bgain, 1, rgain
            };

                using (var mat16UC1 = new Mat(dst.Height, dst.Width, MatType.CV_16UC1, dst.CV_16UC1))
                using (var mat16UC3 = new Mat(dst.Height, dst.Width, MatType.CV_16UC3, dst.CV_16UC3))
                using (var matMatrix = new Mat(3, 3, MatType.CV_32FC1, dst.matrix))
                using (var matGain = new Mat(3, 3, MatType.CV_32FC1, gain))
                {
                    switch (bayer)
                    {
                        case Bayer.BG:
                            Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerBG2BGR);
                            break;
                        case Bayer.GB:
                            Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerGB2BGR);
                            break;
                        case Bayer.RG:
                            Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerRG2BGR);
                            break;
                        default:
                            Cv2.CvtColor(mat16UC1, mat16UC3, ColorConversionCodes.BayerGR2BGR);
                            break;
                    }
                    Cv2.Transform(mat16UC3, mat16UC3, matMatrix.Mul(matGain));

                    #region MyRegion

                    // def 4970 線形変換のみ
                    //for (int i = 0; i < dst.CV_16UC3.Length; i++)
                    //{
                    //    var hoge = dst.CV_16UC3[i] >> 8;
                    //    dst.CV_8UC3[i] = hoge > byte.MaxValue ? byte.MaxValue : (byte)hoge;
                    //}

                    //8bit -> 8bitのみなので却下
                    //Cv2.LUT(mat16UC3, dst.LUT, mat16UC3);

                    //5446
                    //for (int i = 0; i < dst.CV_16UC3.Length; i++)
                    //{
                    //    dst.CV_8UC3[i] = dst.LUT[dst.CV_16UC3[i]];
                    //}

                    #endregion

                    if (parallel)
                    {
                        //4004
                        Parallel.For(0, dst.Height, y =>
                        {
                            for (int x = y * dst.Width * 3; x < y * dst.Width * 3 + dst.Width * 3; x++)
                            {
                                dst.CV_8UC3[x] = dst.LUT[dst.CV_16UC3[x]];
                            }
                        });
                    }
                    else
                    {
                        //4638
                        unsafe
                        {
                            fixed (UInt16* pin16 = dst.CV_16UC3)
                            fixed (byte* pin = dst.LUT)
                            fixed (Byte* pin8 = dst.CV_8UC3)
                            {
                                var p16 = pin16;
                                var p8 = pin8;
                                var length = pin16 + dst.CV_16UC3.Length;
                                while (p16 < length)
                                {
                                    //*p8++ = dst.LUT[*p16++];
                                    *p8++ = *(pin + *p16++);
                                }
                            }
                        }
                    }
                }
            }

            private static void DevelopingColor2(PixelDevelopingOpenCV dst, Bayer bayer, float rgain, float bgain, bool parallel)
            {
                var gain = new float[] {
                bgain, 1, rgain,
                bgain, 1, rgain,
                bgain, 1, rgain
            };

                //var M1 = DenseMatrix.OfArray(new double[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 9, 8 } });

                using (var mat16UC3 = new Mat(dst.Height, dst.Width, MatType.CV_16UC3, dst.CV_16UC3))
                {

                    if (parallel)
                    {
                        Parallel.For(0, dst.Height, y =>
                        {
                            int s = y * dst.Width;
                            var span1 = new ReadOnlySpan<ushort>(dst.CV_16UC1).Slice(s + 1, dst.Width - 1);
                            var span2 = new ReadOnlySpan<ushort>(dst.CV_16UC1).Slice(s + 1, dst.Width - 1);
                            var span3 = new ReadOnlySpan<ushort>(dst.CV_16UC1).Slice(s + 1, dst.Width - 1);

                            for (int x = y * dst.Width * 3; x < y * dst.Width * 3 + dst.Width * 3; x++)
                            {
                                var index = 3 * (x + y * dst.Width);

                                /*B*/
                                dst.CV_8UC3[index + 0] = dst.LUT[dst.CV_16UC3[x]];
                                /*G*/
                                dst.CV_8UC3[index + 1] = dst.LUT[dst.CV_16UC3[x]];
                                /*R*/
                                dst.CV_8UC3[index + 2] = dst.LUT[dst.CV_16UC3[x]];
                            }
                        });
                    }
                    else
                    {

                    }
                }
            }
        }

    }
}




//    public static class Developing
//    {
//        public struct Params
//        {
//            public int width;
//            public int height;
//            public int bitshift;
//        }

//        //var dst = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);
//        public static void ToMono(this WriteableBitmap dst, int[] src, Params param)
//        {
//            byte[] mat = new byte[param.width * param.height * 3];

//            using (Mat matsrc = new Mat(param.height, param.width, MatType.CV_8UC3, mat))
//            {
//                for (int y = 0; y < param.height; y++)
//                for (int x = 0; x < param.width; x++)
//                {
//                    var index = x + y * param.width;
//                    var hogehoge = src[index] >> param.bitshift;
//                    var hoge = (byte)(hogehoge > 255 ? 255 : hogehoge < 0 ? 0 : hogehoge);
//                    mat[index * 3] = hoge;
//                    mat[index * 3 + 1] = hoge;
//                    mat[index * 3 + 2] = hoge;
//                }
//                WriteableBitmapConverter.ToWriteableBitmap(matsrc, dst);
//            }
//        }
//    }

//    public static class ToBitmap
//    {
//        public static float[] m = new float[9] {
//             2.59f ,0 ,0
//            ,0     ,1 ,0
//            ,0     ,0 ,2.08f
//        };

//        public static WriteableBitmap Mono(int[] src, int width, int height)
//        {
//            byte[] mat = new byte[width * height * 3];
//            var dst = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);

//            for (int i = 0; i < src.Length; i++)
//            {
//                var hoge = (byte)(src[i] > 255 ? 255 : src[i] < 0 ? 0 : src[i]);
//                mat[i * 3] = hoge;
//                mat[i * 3 + 1] = hoge;
//                mat[i * 3 + 2] = hoge;
//            }
//            using (Mat matsrc = new Mat(height, width, MatType.CV_8UC3, mat))
//            {
//                WriteableBitmapConverter.ToWriteableBitmap(matsrc, dst);
//            }
//            return dst;
//        }

//        public static WriteableBitmap Color(int[] src, int width, int height)
//        {
//            byte[] mat = new byte[width * height * 1];
//            byte[] mat_c = new byte[width * height * 3];
//            var dst = new WriteableBitmap(width, height, 96, 96, System.Windows.Media.PixelFormats.Bgr24, null);

//            for (int y = 0; y < height; y += 2)
//            {
//                for (int x = 0; x < width; x++)
//                {
//                    var i = x + y * width;
//                    var hoge = (byte)(src[i] > 255 ? 255 : src[i] < 0 ? 0 : src[i]);
//                    mat[i] = hoge;
//                }
//                for (int x = 1; x < width; x++)
//                {
//                    var i = x + (y + 1) * width;
//                    var hoge = (byte)(src[i] > 255 ? 255 : src[i] < 0 ? 0 : src[i]);
//                    mat[i - 1] = hoge;
//                }
//            }

//            using (Mat matsrc = new Mat(height, width, MatType.CV_8UC1, mat))
//            using (Mat matc = new Mat(height, width, MatType.CV_8UC3, mat_c))
//            using (Mat matrix = new Mat(3, 3, MatType.CV_32FC1, m))
//            {
//                Cv2.CvtColor(matsrc, matc, ColorConversionCodes.BayerGR2BGR);
//                Cv2.Transform(matc, matc, matrix);
//                WriteableBitmapConverter.ToWriteableBitmap(matc, dst);
//            }
//            return dst;
//        }
//    }
//}

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
