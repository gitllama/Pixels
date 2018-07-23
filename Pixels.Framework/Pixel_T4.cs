  
  

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Pixels.Framework
{
    static class Converter
    {

		private static void ConvertParallel<T>(T[] src, UInt16[] dst, int width, int height) where T : struct
        {
            switch (src)
            {
				
                case Int64[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                case Int32[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                case Int16[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                case UInt64[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                case UInt32[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                case UInt16[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                case Single[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                case Double[] p:
				/*	Parallel.For(0, height, y =>
					{
						if ((y & 1) == 0)
						{
							for (int x = y * width; x < ( y + 1 ) * width; x++)
							{
								//var hoge = (src[x] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
						else
						{
							//stagger
							for (int x = y * width + 1; x < ( y + 1 ) * width - 1; x++)
							{
								//var hoge = (src.pix[x + dst.stagger] + dst.offset) >> dst.bitshift;
								var hoge = p[x];
								dst[x] = (UInt16)(hoge > UInt16.MaxValue ? UInt16.MaxValue : hoge < UInt16.MinValue ? UInt16.MinValue : hoge);
							}
						}
					}); */
					break;
				
                default:
                    break;
            }
        }
		
		private static void Convert<T>(T[] src, UInt16[] dst, int width, int height) where T : struct
        {
            switch (src)
            {
				
                case Int64[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                case Int32[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                case Int16[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                case UInt64[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                case UInt32[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                case UInt16[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                case Single[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                case Double[] p:
                    /* for (int y = 0; y < height * width; y += width * 2)
                    {
                        var span = src.pix.AsSpan().Slice(y + 0, width * 2);
                        var span2 = dst.CV_16UC1.AsSpan().Slice(y + 0, width * 2);
                        fixed (int* pin = span)
                        fixed (UInt16* pin2 = span2)
                        {
                            var p = pin;
                            var p2 = pin2;
                            var last = p + width;
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
                    } */
					break;
				
                default:
                    break;
            }
        }
    }
}