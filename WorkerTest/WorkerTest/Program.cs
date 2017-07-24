using System;
using System.Threading;

namespace WorkerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var worker1 = new Worker("Tran", 10);
            var workerThread1 = new Thread(worker1.DoWork);
            workerThread1.Start("A");

            var worker2 = new Worker("Marry", 15);
            var workerThread2 = new Thread(worker2.DoWork);
            workerThread2.Start("B");

            Console.Read();
        }
    }
}
