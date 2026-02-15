using System;
using System.Threading;

namespace AbortChildThread
{
    class Program
    {
        static void Main(string[] args)
        {
            BoxingUnboxingDemo.Run();
            Console.WriteLine();

            Console.WriteLine("Da luong trong C#");
            Console.WriteLine("Vi du minh hoa huy Thread");
            Console.WriteLine("-------------------------------------");

            using var cts = new CancellationTokenSource();
            var token = cts.Token;

            Console.WriteLine("Trong Main Thread: tao Thread con.");
            var childThread = new Thread(() => CallToChildThread(token));
            childThread.Start();

            Thread.Sleep(2000);

            Console.WriteLine("Trong Main Thread: yeu cau dung Thread con.");
            cts.Cancel();
            childThread.Join();

            if (!Console.IsInputRedirected)
            {
                Console.ReadKey();
            }
        }

        public static void CallToChildThread(CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Bat dau Thread con!!!");

                for (var i = 0; i <= 10; i++)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(500);
                    Console.WriteLine(i);
                }

                Console.WriteLine("Thread con hoan thanh.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Thread con da nhan yeu cau dung.");
            }
            finally
            {
                Console.WriteLine("Thread con ket thuc.");
            }
        }
    }
}
