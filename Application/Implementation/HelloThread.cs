using Application.Interface;
using System;
using System.Threading;

namespace Application.Implementation
{
    class HelloThread : IProgram
    {
        public void Run()
        {
            Console.WriteLine("Create new Thread writing B...\n");
            var newThread = new Thread(WriteB);

            Console.WriteLine("Start newThread writing B...\n");
            newThread.Start();

            Console.WriteLine("Call Write('-') in main Thread...\n");
            for (int i = 0; i < 50; i++)
            {
                Console.Write("-");
                Thread.Sleep(70);
            }

            Console.WriteLine("\nMain Thread finished!\n");
        }

        public void WriteB()
        {
            for (var i = 0; i < 100; i++)
            {
                Console.Write("B");
                Thread.Sleep(100);
            }
        }
    }
}
