using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Standard
{
    public class PixelDeveloping
    {
        public readonly int Width;
        public readonly int Height;

        public float[] matrix;

        public (float R, float G, float B) Gain;

        public Bayer bayer = Bayer.Mono;

        public (double gamma, int max) Gamma { get; private set; }
        public byte[] LUT;

        public int offset;
        public int bitshift;
        public (int GR, int BG) stagger;

        public PixelDeveloping(int width, int height)
        {
            Width = width;
            Height = height;

            matrix = new float[9] {
                 1f ,0  ,0
                ,0  ,1f ,0
                ,0  ,0  ,1f
            };

            offset = 0;
            bitshift = 0;
            stagger = (0, 0);
            LUT = new byte[UInt16.MaxValue + 1];
            SetGamma(1);
        }

        public void SetGamma(double gamma, int max = Byte.MaxValue)
        {
            if (Gamma != (gamma, max))
            {
                for (var i = 0; i <= max; i++)
                {
                    LUT[i] = (byte)(255.0 * Math.Pow(((double)i / max), 1 / gamma));
                }
                for (var i = max + 1; i <= UInt16.MaxValue; i++)
                {
                    LUT[i] = (byte)byte.MaxValue;
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

    }

    public static class PixelDevelopExtented
    {
        public static void Developing<T>(this PixelDeveloping pd, Pixel<T> src, IntPtr dst, string plane = "Full", bool parallel = false) where T : struct
        {
            //if (src is Pixel<int> pixs)
                //Demosaic.Run(pixs, pd, plane);
        }

        public static class _Demosaic
        {
            public static void Run(Pixel<int> src, PixelDeveloping dst, string plane = "Full")
            {
                var p = src.GetPlane(plane);
                int x = p.left;
                int y = p.top;
                int width = p.width;
                int height = p.height;
                (int x, int y)[] start;
                (int x, int y)[] length = new(int x, int y)[]
                {
                    (width - 2, height - 2),
                    (width - 2, height - 2),
                    (width - 2, height - 2),
                    (width - 2, height - 2)
                }; ;

                switch (dst.bayer)
                {
                    case Bayer.GR:
                        start = new(int x, int y)[]
                        {
                            (x + 1, y + 1),
                            (x + 2, y + 1),
                            (x + 1, y + 2),
                            (x + 2, y + 2)
                        };
                        break;
                    case Bayer.RG:
                        start = new(int x, int y)[]
                        {
                            (x + 2, y + 1),
                            (x + 1, y + 1),
                            (x + 2, y + 2),
                            (x + 1, y + 2)
                        };
                        break;
                    case Bayer.BG:
                        start = new(int x, int y)[]
                        {
                            (x + 1, y + 2),
                            (x + 2, y + 2),
                            (x + 1, y + 1),
                            (x + 2, y + 1)
                        };
                        break;
                    case Bayer.GB:
                        start = new(int x, int y)[]
                        {
                            (x + 2, y + 2),
                            (x + 1, y + 2),
                            (x + 2, y + 1),
                            (x + 1, y + 1)
                        };
                        break;
                    default:
                        start = new(int x, int y)[]{};
                        break;
                }
                switch (dst.bayer)
                {
                    case Bayer.RG:
                    case Bayer.GR:
                    case Bayer.GB:
                    case Bayer.BG:
                        //DemosaicColor(src.pix, dst.CV_8UC3, start, length, src.Width, dst.GetMatrix(), dst.stagger);
                        break;
                    default:
                        //DemosaicMono(src.pix, dst.CV_8UC3, (x, y), (width, height), src.Width);
                        break;
                }

            }

            static void Set(float R, float G, float B, float[] matrix, byte[] dst, int index)
            {
                float BB = matrix[0] * B + matrix[1] * G + matrix[2] * R;
                float GG = matrix[3] * B + matrix[4] * G + matrix[5] * R;
                float RR = matrix[6] * B + matrix[7] * G + matrix[8] * R;
                dst[index] = (byte)(BB > Byte.MaxValue ? Byte.MaxValue : BB < Byte.MinValue ? 0 : BB);
                dst[index + 1] = (byte)(GG > Byte.MaxValue ? Byte.MaxValue : GG < Byte.MinValue ? 0 : GG);
                dst[index + 2] = (byte)(RR > Byte.MaxValue ? Byte.MaxValue : RR < Byte.MinValue ? 0 : RR);
            }

            static void DemosaicColor(int[] src, byte[] dst, (int x, int y)[] starts, (int x, int y)[] lengths, int width, float[] matrix, (int GR, int BG) stagger)
            {
                (int x, int y) start;
                (int x, int y) length;

        
                //Gr
                start = starts[0];
                length = lengths[0];
                Parallel.For(0, length.y / 2, y =>
                {
                    int indexY = (start.y + y * 2) * width;
                    var top = src.AsSpan().Slice(indexY - width + stagger.BG);
                    var mid = src.AsSpan().Slice(indexY + stagger.GR);
                    var bot = src.AsSpan().Slice(indexY + width + stagger.BG);
                    for (int x = start.x; x < start.x + length.x; x += 2)
                    {
                        float G = (float)mid[x] / 2.0f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

                        float R = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                        float B = ((float)top[x] + (float)bot[x]) / 2f;

                        Set(R, G, B, matrix, dst, (indexY + x) * 3);
                    }
                });
                //R
                start = starts[1];
                length = lengths[1];
                Parallel.For(0, length.y / 2, y =>
                {
                    int indexY = (start.y + y * 2) * width;
                    var top = src.AsSpan().Slice(indexY - width + stagger.BG);
                    var mid = src.AsSpan().Slice(indexY + stagger.GR);
                    var bot = src.AsSpan().Slice(indexY + width + stagger.BG);
                    for (int x = start.x; x < start.x + length.x; x += 2)
                    {
                        float R = (float)mid[x];
                        float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                        float B = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                        Set(R, G, B, matrix, dst, (indexY + x) * 3);
                    }
                });
                //B
                start = starts[2];
                length = lengths[2];
                Parallel.For(0, length.y / 2, y =>
                {
                    int indexY = (start.y + y * 2) * width;
                    var top = src.AsSpan().Slice(indexY - width + stagger.GR);
                    var mid = src.AsSpan().Slice(indexY + stagger.BG);
                    var bot = src.AsSpan().Slice(indexY + width + stagger.GR);
                    for (int x = start.x; x < start.x + length.x; x += 2)
                    {
                        float B = (float)mid[x];
                        float G = ((float)top[x] + (float)mid[x - 1] + (float)mid[x + 1] + (float)bot[x]) / 4f;
                        float R = ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 4f;

                        Set(R, G, B, matrix, dst, (indexY + x) * 3);
                    }
                });
                //Gb
                start = starts[3];
                length = lengths[3];
                Parallel.For(0, length.y / 2, y =>
                {
                    int indexY = (start.y + y * 2) * width;
                    var top = src.AsSpan().Slice(indexY - width + stagger.GR);
                    var mid = src.AsSpan().Slice(indexY + stagger.BG);
                    var bot = src.AsSpan().Slice(indexY + width + stagger.GR);
                    for (int x = start.x; x < start.x + length.x; x += 2)
                    {
                        float G = (float)mid[x] / 2f + ((float)top[x - 1] + (float)top[x + 1] + (float)bot[x - 1] + (float)bot[x + 1]) / 8f;

                        float B = ((float)mid[x - 1] + (float)mid[x + 1]) / 2f;
                        float R = ((float)top[x] + (float)bot[x]) / 2f;

                        Set(R, G, B, matrix, dst, (indexY + x) * 3);
                    }
                });
            }



            static void DemosaicMono(int[] src, byte[] dst, (int x, int y) start, (int x, int y) length, int width)
            {
                Parallel.For(0, length.y, y =>
                {
                    int indexY = (start.y + y) * width;
                    var mid = src.AsSpan().Slice(indexY);
                    for (int x = start.x; x < start.x + length.x; x++)
                    {
                        int M = mid[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        dst[(indexY + x) * 3] = N;
                        dst[(indexY + x) * 3 + 1] = N;
                        dst[(indexY + x) * 3 + 2] = N;
                    }
                });
            }
        }

        //LUTの実装
    }


    public static class PixelDeveloper
    {

        public static unsafe void Demosaic(Pixel<int> src, IntPtr pin, int stride, bool parallel)
        {
            int channel = 3;
            int residue = stride - channel * src.Width;
            int width = src.Width;
            int height = src.Height;
            if (parallel)
            {
                Parallel.For(0, src.Height, y =>
                {
                    byte* p = (byte*)(pin);
                    p += y * stride;
                    var span = src.pix.AsSpan().Slice(y * width, width);
                    for (int x = 0; x < span.Length; x++)
                    {
                        int M = span[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);
                        //*p++ = ((N << 16) | (N << 8) | N);
                        *p++ = N;
                        *p++ = N;
                        *p++ = N;
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
                        int M = src[x];
                        byte N = (byte)(M > Byte.MaxValue ? Byte.MaxValue : M < Byte.MinValue ? 0 : M);

                        *(p++) = N;
                        *(p++) = N;
                        *(p++) = N;
                    }
                    p += residue;
                }
            }
        }
    }
}
