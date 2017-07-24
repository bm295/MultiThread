using System;
using System.Threading;

namespace ThreadParamDemo
{
    class MyWork
    {
        public static void DoWork(object ch)
        {
            for (var i = 0; i < 100; i++)
            {
                Console.Write(ch);
                Thread.Sleep(50);
            }
        }
    }
}
