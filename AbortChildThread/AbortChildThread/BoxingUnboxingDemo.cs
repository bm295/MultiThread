using System;

namespace AbortChildThread
{
    public static class BoxingUnboxingDemo
    {
        public const string BoxingDefinition =
            "Boxing is converting a value type (e.g., int) to object (or an interface) by wrapping it in a heap-allocated object.";

        public const string UnboxingDefinition =
            "Unboxing is extracting the value type from an object reference and requires an explicit cast to the original value type.";

        public static void Run()
        {
            int value = 42;
            object boxed = value; // Boxing: copy value into a new object on the heap.

            value++; // Change original value after boxing.
            int castResult = (int)boxed;
            castResult++; // Incrementing cast result does not update boxed object.

            int unboxed = (int)boxed; // Unboxing: copy value out of boxed object.
            unboxed++; // Change unboxed local copy.

            Console.WriteLine("=== Boxing and Unboxing Demo ===");
            Console.WriteLine("Boxing: " + BoxingDefinition);
            Console.WriteLine("Unboxing: " + UnboxingDefinition);
            Console.WriteLine($"Original value after value++: {value}");
            Console.WriteLine($"Boxed value (still original boxed copy): {boxed}");
            Console.WriteLine($"Cast result after castResult++: {castResult}");
            Console.WriteLine($"Unboxed value after unboxed++: {unboxed}");
            Console.WriteLine("Takeaway: boxing/unboxing copies values, they do not share storage.");
        }
    }
}
