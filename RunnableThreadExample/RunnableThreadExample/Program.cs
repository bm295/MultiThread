using System;
using System.Threading;

namespace RunnableThreadExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var instance = new RunnableThreadObject();
            var thread = new Thread(instance.Run);
            thread.Start();

            while (instance.count != 5)
            {
                try
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("instance.count = " + instance.count);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }

            Console.ReadKey();
        }
    }
}
