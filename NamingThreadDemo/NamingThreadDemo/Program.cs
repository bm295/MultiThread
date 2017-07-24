using System;
using System.Threading;

namespace NamingThreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";

            Console.WriteLine("Code of " + Thread.CurrentThread.Name);

            Console.WriteLine("Create new thread");
            var letgoThread = new Thread(LetGo);
            letgoThread.Name = "Let's Go";
            letgoThread.Start();

            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine("Code of " + Thread.CurrentThread.Name);
                Thread.Sleep(30);
            }

            Console.Read();
        }

        public static void LetGo()
        {
            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine("Code of " + Thread.CurrentThread.Name);
                Thread.Sleep(50);
            }
        }
    }
}
