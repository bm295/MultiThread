using System;
using System.Threading;

namespace ThreadParamDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create new thread.. \n");
            var workThread = new Thread(MyWork.DoWork);

            Console.WriteLine("Start workThread...\n");
            workThread.Start('*');

            for (var i = 0; i < 20; i++)
            {
                Console.Write(".");
                Thread.Sleep(30);
            }

            Console.WriteLine("MainThread ends");
            Console.Read();
        }
    }
}
