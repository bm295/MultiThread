using System;
using System.Threading;

namespace ThreadYieldDemo
{
    class Program
    {
        private static DateTime importantEndTime;
        private static DateTime unimportantEndTime;

        static void Main(string[] args)
        {
            importantEndTime = DateTime.Now;
            unimportantEndTime = DateTime.Now;

            Console.WriteLine("Create thread 1");
            var thread1 = new Thread(ImportantWork)
            {
                Priority = ThreadPriority.Highest
            };

            Console.WriteLine("Create thread 2");
            var thread2 = new Thread(UnimportantWork)
            {
                Priority = ThreadPriority.Lowest
            };

            thread2.Start();
            thread1.Start();

            Console.Read();
        }

        public static void ImportantWork()
        {
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine("\n Important work " + i);
                Thread.Yield();
            }
            importantEndTime = DateTime.Now;
            PrintTime();
        }

        public static void UnimportantWork()
        {
            for (int i = 0; i < 100000; i++)
            {
                Console.WriteLine("\n  -- UnImportant work " + i);
            }
            unimportantEndTime = DateTime.Now;
            PrintTime();
        }

        private static void PrintTime()
        {
            var interval = unimportantEndTime - importantEndTime;
            Console.WriteLine("UnImportant Thread - Important Thread = " + interval.TotalMilliseconds + " milliseconds");
        }
    }
}
