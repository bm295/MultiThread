using System;
using System.Threading;

namespace ChildThread
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Da luong trong C#");
            Console.WriteLine("Vi du minh hoa cach tao Thread");
            Console.WriteLine("----------------------------------");

            var childRef = new ThreadStart(CallToChildThread);

            Console.WriteLine("Trong Main Thread: tao thread con.");
            var childThread = new Thread(childRef);
            childThread.Start();

            Console.ReadKey();
        }

        public static void CallToChildThread()
        {
            Console.WriteLine("Thread con bat dau!!!");

            int sleepfor = 5000;
            Console.WriteLine("Thread con dung trong khoang {0} giay", sleepfor / 1000);
            Thread.Sleep(sleepfor);
            Console.WriteLine("Thread con phuc hoi!!!");
        }
    }
}
