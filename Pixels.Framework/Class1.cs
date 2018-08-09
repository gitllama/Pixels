using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pixels.Framework
{
    public static class TaskBuilder
    {
        public class TaskOptions
        {
            public int millisecondsTimeout;

        }

        public static void Run(Action func, Action func2, Action action, CancellationToken token = default, TaskOptions options = null)
        {
            var ioTask = new Task<DateTime>(()=>
            {
                func();
                return DateTime.Now;
            });
            var devlopTask = new Task<DateTime>(() =>
            {
                func2();
                return DateTime.Now;
            });


            var mainTask = Task.Factory.StartNew(() =>
            {
                while (token != default && !token.IsCancellationRequested)
                {
                    var time = DateTime.Now;
                    Thread.Sleep(options.millisecondsTimeout);

                    var ts = Task.WhenAll(ioTask, devlopTask);
                    var result = ts.Result;

                    var ts1 = 1000 / result[0].Subtract(time).TotalMilliseconds;
                    var ts2 = 1000 / result[1].Subtract(time).TotalMilliseconds;

                    //Buffer.BlockCopy(img_uint2int.img, 0, src, 0, 4 * img_uint2int.img.Length);


                }
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);

            mainTask.ContinueWith(x => {
                action();
                //src = null;
                //CloseIOToken();
            });
        }
    }
}

//var task1 = Task.Run(() =>
//{
//    Console.WriteLine(p.Value1);
//});
//var task2 = Task.Run(() =>
//{
//    Console.WriteLine(p.Value1);
//});
//Task.WhenAll(task1, task2).Wait();