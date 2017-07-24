using System;
using System.Threading.Tasks;

namespace Async01
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Example();
                string result = Console.ReadLine();
                Console.WriteLine("You type: " + result);
            }
        }

        private static async void Example()
        {
            int t = await Task.Run(() => Allocate());
            Console.WriteLine("Compute " + t);
        }

        private static int Allocate()
        {
            int size = 0;
            for (int z = 0; z < 100; z++)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    string value = i.ToString();
                    size += value.Length;
                }
            }
            return size;
        }
    }
}
