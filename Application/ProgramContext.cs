using Application.Implementation;
using Application.Implementation.NestedLockDeadlock;
using Application.Interface;
using System;
using System.Collections.Generic;

namespace Application
{
    public class ProgramContext
    {
        private List<IProgram> _programs;

        public ProgramContext()
        {
            _programs = new List<IProgram>
            {
                new NestedLockDeadlock(),
                new HelloThread()
            };
        }

        public void ShowAllOptions()
        {
            for (var i = 0; i < _programs.Count; i++)
            {
                Console.WriteLine($"Option {i}: {_programs[i].GetType().Name}");
            }
        }

        public void RunWith(int option)
        {
            _programs[option].Run();
        }
    }
}
