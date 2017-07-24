using System;
using System.Threading;

namespace WorkerTest
{
    class Worker
    {
        private string _name;
        private int _loop;

        public Worker(string name, int loop)
        {
            _name = name;
            _loop = loop;
        }

        public void DoWork(object value)
        {
            for (var i = 0; i < _loop; i++)
            {
                Console.WriteLine(_name + " working " + value);
                Thread.Sleep(50);
            }
        }
    }
}
