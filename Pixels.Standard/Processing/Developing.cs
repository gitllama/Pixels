/* Code generated using the t4 templates <# 
    var src = File.ReadAllText(Host.ResolvePath(targetPath)); 
    var m = Regex.Matches(src, @"T4\[(?<key>[\s\S]*?)\]\{(?<value>[\s\S]*?)\/\/\}T4");
    var methods = m.Cast<Match>().ToDictionary<Match, string, string>(k => k.Groups["key"].Value, v => v.Groups["value"].Value);
#>*/
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Standard.Processing
{
    #region A
    /*<#/*/

    public class Options
    {
        public float[] matrix;

        public (float R, float G, float B) Gain;

        public Bayer bayer = Bayer.Mono;

        public (double gamma, int max) Gamma { get; private set; }
        public byte[] _LUT;

        public int offset;
        public int bitshift;
        public (int GR, int BG) stagger;

        public bool parallel = false;

        public Options()
        {
            matrix = new float[9] {
                 1f ,0  ,0
                ,0  ,1f ,0
                ,0  ,0  ,1f
            };

            offset = 0;
            bitshift = 0;
            stagger = (0, 0);
            _LUT = new byte[UInt16.MaxValue + 1];
            SetGamma(1);
        }



        public void SetGamma(double gamma, int max = Byte.MaxValue)
        {
            if (Gamma != (gamma, max))
            {
                for (var i = 0; i <= max; i++)
                {
                    _LUT[i] = (byte)(255.0 * Math.Pow(((double)i / max), 1 / gamma));
                }
                for (var i = max + 1; i <= UInt16.MaxValue; i++)
                {
                    _LUT[i] = (byte)byte.MaxValue;
                }
            }
        }

        public void SetGain(float R, float G, float B)
        {
            if (Gain != (R, G, B))
            {
                Gain = (R, G, B);
            }
        }
        public float[] GetMatrix()
        {
            return new float[]
            {
                matrix[0] * Gain.B, matrix[1] * Gain.B, matrix[2] * Gain.B,
                matrix[3] * Gain.G, matrix[4] * Gain.G, matrix[5] * Gain.G,
                matrix[6] * Gain.R, matrix[7] * Gain.R, matrix[8] * Gain.R,
            };
        }

        public byte LUT(int value) => _LUT[value > _LUT.Length ? _LUT.Length - 1 : value < 0 ? 0 : value];

        public (int left, int top, int width, int height)[] GetOrigins<T>(Pixel<T> src) where T : struct
        {
            var p = src.GetPlane("Full");
            int x = p.left;
            int y = p.top;
            int width = p.width;
            int height = p.height;
            switch (bayer)
            {
                case Bayer.GR:
                    return new(int, int, int, int)[]
                    {
                        (x + 1, y + 1, width - 2, height - 2),
                        (x + 2, y + 1, width - 2, height - 2),
                        (x + 1, y + 2, width - 2, height - 2),
                        (x + 2, y + 2, width - 2, height - 2)
                    };
                case Bayer.RG:
                    return new(int, int, int, int)[]
                    {
                        (x + 2, y + 1, width - 2, height - 2),
                        (x + 1, y + 1, width - 2, height - 2),
                        (x + 2, y + 2, width - 2, height - 2),
                        (x + 1, y + 2, width - 2, height - 2)
                    };
                case Bayer.BG:
                    return new(int, int, int, int)[]
                    {
                        (x + 1, y + 2, width - 2, height - 2),
                        (x + 2, y + 2, width - 2, height - 2),
                        (x + 1, y + 1, width - 2, height - 2),
                        (x + 2, y + 1, width - 2, height - 2)
                    };
                case Bayer.GB:
                    return new(int, int, int, int)[]
                    {
                        (x + 2, y + 2, width - 2, height - 2),
                        (x + 1, y + 2, width - 2, height - 2),
                        (x + 2, y + 1, width - 2, height - 2),
                        (x + 1, y + 1, width - 2, height - 2)
                    };
                default:
                    return null;
            }
        }
    }

    public static class PixelDevelopExtented
    {

        public static class _Demosaic
        {


            //static void DemosaicColor(int[] src, byte[] dst, (int x, int y)[] starts, (int x, int y)[] lengths, int width, float[] matrix, (int GR, int BG) stagger)
            //{
            //    (int x, int y) start;
            //    (int x, int y) length;

        
            //    //Gr
            //    start = starts[0];
            //    length = lengths[0];
            //    Parallel.For(0, length.y / 2, y =>
            //    {
            //        int indexY = (start.y + y * 2) * width;
            //        var top = src.AsSpan().Slice(indexY - width + stagger.BG);
            //        var mid = src.AsSpan().Slice(indexY + stagger.GR);
            //        var bot = src.AsSpan().Slice(indexY + width + stagger.BG);
            //        for (int x = start.x; x < start.x + length.x; x += 2)
            //        {
            //            float G = (float)mid[x] / 2.0f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

            //            float R = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
            //            float B = ((float)top[x] + (float)bot[x]) / 2f;

            //            Set(R, G, B, matrix, dst, (indexY + x) * 3);
            //        }
            //    });
            //    //R
            //    start = starts[1];
            //    length = lengths[1];
            //    Parallel.For(0, length.y / 2, y =>
            //    {
            //        int indexY = (start.y + y * 2) * width;
            //        var top = src.AsSpan().Slice(indexY - width + stagger.BG);
            //        var mid = src.AsSpan().Slice(indexY + stagger.GR);
            //        var bot = src.AsSpan().Slice(indexY + width + stagger.BG);
            //        for (int x = start.x; x < start.x + length.x; x += 2)
            //        {
            //            float R = (float)mid[x];
            //            float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
            //            float B = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

            //            Set(R, G, B, matrix, dst, (indexY + x) * 3);
            //        }
            //    });
            //    //B
            //    start = starts[2];
            //    length = lengths[2];
            //    Parallel.For(0, length.y / 2, y =>
            //    {
            //        int indexY = (start.y + y * 2) * width;
            //        var top = src.AsSpan().Slice(indexY - width + stagger.GR);
            //        var mid = src.AsSpan().Slice(indexY + stagger.BG);
            //        var bot = src.AsSpan().Slice(indexY + width + stagger.GR);
            //        for (int x = start.x; x < start.x + length.x; x += 2)
            //        {
            //            float B = (float)mid[x];
            //            float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
            //            float R = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

            //            Set(R, G, B, matrix, dst, (indexY + x) * 3);
            //        }
            //    });
            //    //Gb
            //    start = starts[3];
            //    length = lengths[3];
            //    Parallel.For(0, length.y / 2, y =>
            //    {
            //        int indexY = (start.y + y * 2) * width;
            //        var top = src.AsSpan().Slice(indexY - width + stagger.GR);
            //        var mid = src.AsSpan().Slice(indexY + stagger.BG);
            //        var bot = src.AsSpan().Slice(indexY + width + stagger.GR);
            //        for (int x = start.x; x < start.x + length.x; x += 2)
            //        {
            //            float G = (float)mid[x] / 2f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

            //            float B = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
            //            float R = ((float)top[x] + (float)bot[x]) / 2f;

            //            Set(R, G, B, matrix, dst, (indexY + x) * 3);
            //        }
            //    });
            //}




        }

    }


    /*/#>*/
    #endregion

    #region T4

    public static partial class PixelDeveloper
    {
        #region MyRegion

        // <#/* T4[A]{
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicMono(Pixel<Int32> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (option.parallel)
            {
                Parallel.For(0, src.Height, y =>
                {
                    byte* p = (byte*)(pin);
                    p += y * stride;
                    var span = src.pix.AsSpan().Slice(y * width, width);
                    for (int x = 0; x < span.Length; x++)
                    {
                        var M = option.LUT((int)span[x]);
                        // byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        // *p++ = ((N << 16) | (N << 8) | N);
                        *p++ = M;
                        *p++ = M;
                        *p++ = M;
                    }
                });
            }
            else
            {
                var p = (byte*)pin;
                for (int y = 0; y < height; ++y)
                {
                    for (int x = y * width; x < y * width + width; ++x)
                    {
                        var M = option.LUT((int)src.pix[x]);
                        *(p++) = M;
                        *(p++) = M;
                        *(p++) = M;
                    }
                    p += residue;
                }
            }
        }
        //}T4 */#>

        // <#/* T4[B]{ 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicColor(Pixel<Int32> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (option.parallel)
            {
                DemosaicColorParallel(src, pin, stride, option);
            }
            else
            {
                var p = (byte*)pin;
                for (int y = 0; y < height; ++y)
                {
                    for (int x = y * width; x < y * width + width; ++x)
                    {
                        var M = src[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);

                        *(p++) = N;
                        *(p++) = N;
                        *(p++) = N;
                    }
                    p += residue;
                }
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void DemosaicColorParallel(Pixel<Int32> src, IntPtr pin, int stride, Options option)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            var origins = option.GetOrigins(src);
            (int left, int top, int width, int height) origin;

            origin = origins[0]; //Gr
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += y * stride;
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.BG);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.GR);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.BG);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float G = (float)mid[x] / 2.0f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

                    float R = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                    float B = ((float)top[x] + (float)bot[x]) / 2f;

                    Set(R, G, B, dst., (indexY + x) * 3, option);
                }
            });
            origin = origins[1]; //R
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += y * stride;
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.BG);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.GR);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.BG);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float R = (float)mid[x];
                    float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                    float B = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                    Set(R, G, B, dst, (indexY + x) * 3, option);
                }
            });
            origin = origins[2]; //B
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += y * stride;
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.GR);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.BG);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.GR);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float B = (float)mid[x];
                    float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                    float R = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                    Set(R, G, B, dst, (indexY + x) * 3, option);
                }
            });
            origin = origins[3]; //Gb
            Parallel.For(0, origin.height / 2, y =>
            {
                byte* p = (byte*)(pin);
                p += (origin.top + y * 2) * stride;
                int indexY = (origin.top + y * 2) * width;
                var top = src.pix.AsSpan().Slice(indexY - width + option.stagger.GR);
                var mid = src.pix.AsSpan().Slice(indexY + option.stagger.BG);
                var bot = src.pix.AsSpan().Slice(indexY + width + option.stagger.GR);
                for (int x = origin.left; x < origin.left + origin.width; x += 2)
                {
                    float G = (float)mid[x] / 2f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

                    float B = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                    float R = ((float)top[x] + (float)bot[x]) / 2f;

                    Set(R, G, B, p, option);
                }
            });
        }


        static void Set(float R, float G, float B, byte* dst, Options option)
        {
            //    float BB = matrix[0] * B + matrix[1] * G + matrix[2] * R;
            //    float GG = matrix[3] * B + matrix[4] * G + matrix[5] * R;
            //    float RR = matrix[6] * B + matrix[7] * G + matrix[8] * R;
            float BB = B;
            float GG = G;
            float RR = R;
            *dst++ = option.LUT((int)BB);
            *dst++ = option.LUT((int)GG);
            *dst++ = option.LUT((int)RR);
        }

        //}T4 */#>
        #endregion



        #region T4

        //created <#= methods["A"].Replace("Int32", "Double") #>

        //created <#= methods["A"].Replace("Int32", "Single") #>

        #endregion






    }

    #endregion

}

// エリア限定する際
//static void DemosaicMono(int[] src, byte[] dst, (int x, int y) start, (int x, int y) length, int width)
//{
//    Parallel.For(0, length.y, y =>
//    {
//        int indexY = (start.y + y) * width;
//        var mid = src.AsSpan().Slice(indexY);
//        for (int x = start.x; x < start.x + length.x; x++)
//        {
//            int M = mid[x];
//            byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
//            dst[(indexY + x) * 3] = N;
//            dst[(indexY + x) * 3 + 1] = N;
//            dst[(indexY + x) * 3 + 2] = N;
//        }
//    });
//}