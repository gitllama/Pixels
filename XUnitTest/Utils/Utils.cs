using Pixels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;

namespace XUnitTest
{
    public static class Utils
    {
        [Obsolete("BenchmarkDotNetを使用してください")]
        public static void Timer(Action action)
        {
            var sw = new System.Diagnostics.Stopwatch();
            sw.Restart();
            action();
            sw.Stop();
            Console.WriteLine($"　{sw.ElapsedMilliseconds}ms");
        }

        // .Net Frameworkなら以下で動くが、.Net Coreの際は工夫必要
        // var pro = new System.Diagnostics.Process();
        // pro.StartInfo.FileName = outputpath;
        // pro.Start();
        public static void CallProcess(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    System.Diagnostics.Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    System.Diagnostics.Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }



        public static dynamic Reflection(this object src)
        {
            return src.GetType().InvokeMember(
                "state",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                null, 
                src, 
                null
            );
        }

        public static void Reflection2(this object src)
        {
            src.GetType().InvokeMember(
                "SumWithState",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, 
                null, 
                src, 
                new object[] { 10 }
            );
        }

        public static object CreatePixel(Type type, int w, int h)
        {
            var constructedType = typeof(Pixel<>).MakeGenericType(type);
            return Activator.CreateInstance(constructedType, w, h);

            //var method = constructedType.GetMethod("Fuga");
            //method.Invoke(sut, null).Is("String");
        }
        public static object CallMethod(Type type, object obj)
        {
            var constructedType = typeof(Pixel<>).MakeGenericType(type);

            var method = constructedType.GetMethod("Fuga");
            return method.Invoke(obj, null);

            //var sut = new Foo();

            //var method = typeof(Foo).GetMethod("Bar");
            //var constructed = method.MakeGenericMethod(typeof(string));
            //constructed.Invoke(sut, null).Is("String");
        }
    }
}
