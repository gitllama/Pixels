using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Processing
{
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

}

