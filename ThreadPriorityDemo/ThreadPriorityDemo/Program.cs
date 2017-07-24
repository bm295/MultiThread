using System;
using System.Threading;

namespace ThreadPriorityDemo
{
    class Program
    {
        private static DateTime endDateTime1;
        private static DateTime endDateTime2;

        static void Main(string[] args)
        {
            endDateTime1 = DateTime.Now;
            endDateTime2 = DateTime.Now;

            var thread1 = new Thread(Hello1)
            {
                Priority = ThreadPriority.Highest
            };
            var thread2 = new Thread(Hello2)
            {
                Priority = ThreadPriority.Lowest
            };
            thread2.Start();
            thread1.Start();

            Console.Read();
        }

        public static void Hello1()
        {
            for (var i = 0; i < 100000; i++)
            {
                Console.WriteLine("Hello from thread 1: " + i);
            }
            endDateTime1 = DateTime.Now;
            PrintInterval();
        }

        public static void Hello2()
        {
            for (var i = 0; i < 100000; i++)
            {
                Console.WriteLine("Hello from thread 2: " + i);
            }
            endDateTime2 = DateTime.Now;
            PrintInterval();
        }

        private static void PrintInterval()
        {
            var interval = endDateTime2 - endDateTime1;
            Console.WriteLine("Thread2 - Thread1 = " + interval.TotalMilliseconds + " milliseconds");
        }
    }
}
