using System;
using System.Threading;

namespace ThreadJoinDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create new thread");

            var letgoThread = new Thread(LetGo);
            letgoThread.Start();
            letgoThread.Join();
            Console.WriteLine("Main thread ends");
            Console.Read();
        }

        public static void LetGo()
        {
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("Let's Go " + i);
            }
        }
    }
}
