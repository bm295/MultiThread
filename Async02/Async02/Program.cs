using System;
using System.Threading.Tasks;

namespace Async02
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Started Main()");
            Task<long> task = TestCountAsync();
            Console.WriteLine("Started counting. Please wait...");
            task.Wait();
            Console.WriteLine("Finished counting");
            Console.ReadLine();
        }

        private static async Task<long> TestCountAsync()
        {
            long k = 0;
            Console.WriteLine("Started TestCountAsync()");
            await Task.Run(() =>
            {
                long countTo = 100000000;
                int prevPercentDone = -1;
                for (int i = 0; i <= countTo; i++)
                {
                    int percentDone = (int) (100 * (i / (double) countTo));
                    if (percentDone != prevPercentDone)
                    {
                        prevPercentDone = percentDone;
                        Console.WriteLine(percentDone + "% ");
                    }
                    k = i;
                }
            });
            Console.WriteLine();
            Console.WriteLine("Finished TestCountAsync()");
            return k;
        }
    }
}
