using System;
using System.Threading;

namespace ThreadStartDemo
{
    class Programmer
    {
        private string _name;
        public Programmer(string name)
        {
            _name = name;
        }

        public void DoCode()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(_name + " coding ... ");
                Thread.Sleep(50);
            }
        }
    }
}
