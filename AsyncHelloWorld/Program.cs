// Program.cs

namespace ProAsyncNet.AsyncHelloWorld
{
    using System;
    using System.Threading.Tasks;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            var task = new Task(Speak);

            task.Start();

            // Comment the following lines to have some fun ...
            Console.WriteLine("Waiting for completion ...");

            task.Wait();

            Console.WriteLine("Task completed.");
            Console.WriteLine();
        }

        private static void Speak()
        {
            Console.WriteLine("Hello Async World!");
        }
    }
}
