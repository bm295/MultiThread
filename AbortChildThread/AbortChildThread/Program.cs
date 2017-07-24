using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbortChildThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Da luong trong C#");
            Console.WriteLine("Vi du minh hoa huy Thread");
            Console.WriteLine("-------------------------------------");

            var childRef = new ThreadStart(CallToChildThread);
            Console.WriteLine("Trong Main Thread: tao Thread con.");
            var childThread = new Thread(childRef);
            childThread.Start();

            Thread.Sleep(2000);

            Console.WriteLine("Trong Main Thread: huy Thread con.");
            childThread.Abort();

            Console.ReadKey();
        }

        public static void CallToChildThread()
        {
            try
            {
                Console.WriteLine("Bat dau Thread con!!!");

                for (var i = 0; i <= 10; i++)
                {
                    Thread.Sleep(500);
                    Console.WriteLine(i);
                }

                Console.WriteLine("Thread con hoan thanh.");
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("Thread Abort Exception!!!");
            }
            finally
            {
                Console.WriteLine("Khong the bat Thread Exception!!!");
            }
        }
    }
}
