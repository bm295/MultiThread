using System;
using System.Threading;

namespace RunnableThreadExample
{
    class RunnableThreadObject
    {
        public int count = 0;

        public void Run()
        {
            Console.WriteLine("Runnable Thread starting");

            try
            {
                while (count < 5)
                {
                    Thread.Sleep(2000);
                    count++;
                    Console.WriteLine("count increases to " + count);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Runnable Thread interrupted");
            }

            Console.WriteLine("Runnable Thread terminating");
        }
    }
}
