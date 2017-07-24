using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadStartDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var threadStart1 = new ThreadStart(DoWork);
            var workThread = new Thread(threadStart1);
            workThread.Start();

            var programmer = new Programmer("Tran");
            var threadStart2 = new ThreadStart(programmer.DoCode);
            var progThread = new Thread(threadStart2);
            progThread.Start();

            Console.WriteLine("Main thread ends");
            Console.Read();
        }

        public static void DoWork()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.Write("*");
                Thread.Sleep(100);
            }
        }
    }
}
