// Program.cs

namespace ProAsyncNet.ClosureLoopTrap
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal static class Program
    {
        static void Main(string[] args)
        {
            // Naive capture in a for-loop.
            Console.WriteLine("Counting from 0 to 9 in tasks (naive for) ...");

            for (int i = 0; i < 10; ++i)
            {
                Task.Factory.StartNew(() =>
                    Console.WriteLine(i));
            }

            PromptUser("Press a key for next try ...");

            // Proper capture in a for-loop.
            Console.WriteLine("Counting from 0 to 9 in tasks (proper 'for') ...");

            for (int i = 0; i < 10; ++i)
            {
                var capturedValue = i;

                Task.Factory.StartNew(() =>
                    Console.WriteLine(capturedValue));
            }

            PromptUser("Press a key for next try ...");

            // Naive "for" but helper method indirectly solves the problem by
            // delaying capture to the proper unique value.
            // Method parameter acts like a capture variable.
            Console.WriteLine("Counting from 0 to 9 in tasks (naive 'for' but using a method) ...");

            for (int i = 0; i < 10; ++i)
            {
                StartWriteLineTask(i);
            }

            PromptUser("Press a key for next try ...");

            // C# 5 capture-behavior in a foreach-loop.
            Console.WriteLine("Counting from 0 to 9 in tasks (C#5 'foreach') ...");

            foreach (var i in Enumerable.Range(0, 10))
            {
                Task.Factory.StartNew(() =>
                    Console.WriteLine(i));
            }

            PromptUser("Press a key to stop program.");
        }

        private static void PromptUser(string prompt)
        {
            Console.WriteLine(prompt);
            Console.ReadKey(true);
            Console.WriteLine();
        }

        /// <summary>
        /// Start a task writing a line of an arbitrary value to the console.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the value.
        /// </typeparam>
        /// <param name="value">
        /// The value to pass to <see cref="System.Console.WriteLine"/>.
        /// </param>
        /// <remarks>
        /// Using this helper method will also enforce a proper closure.
        /// </remarks>
        private static void StartWriteLineTask<T>(T value)
            => Task.Factory.StartNew(() =>
                    Console.WriteLine(value));
    }
}
