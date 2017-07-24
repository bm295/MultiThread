using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadUsingSnippetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create thread 1");
            var newThread1 = new Thread(
                delegate ()
                {
                    for (var i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Code in delegate() " + i);
                        Thread.Sleep(50);
                    }
                }
                );

            Console.WriteLine("Start newThread1");
            newThread1.Start();

            Console.WriteLine("Create thread 2");
            var newThread2 = new Thread
                (
                delegate (object value)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine("Code in delegate(object) " + i + " - " + value);
                        Thread.Sleep(100);
                    }
                }
                );

            Console.WriteLine("Start newThread2");
            newThread2.Start("!!!");

            Console.WriteLine("Main thread ends");
            Console.Read();
        }
    }
}
