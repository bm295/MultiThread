using Application;
using System;

namespace Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var programContext = new ProgramContext();
            programContext.ShowAllOptions();
            Console.Write("Enter option:");
            var option = Convert.ToInt32(Console.ReadLine());
            programContext.RunWith(option);
            Console.ReadKey();
        }
    }
}
