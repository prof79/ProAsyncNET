// Program.cs

namespace ProAsyncNet.ThreadTypes
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting task 1 ...");

            Task.Factory.StartNew(DisplayThreadType).Wait();

            Console.WriteLine("Starting task 2 ...");

            Task.Factory.StartNew(DisplayThreadType, TaskCreationOptions.LongRunning).Wait();

            Console.WriteLine("Program completed.");
            Console.WriteLine();
        }

        private static void DisplayThreadType()
        {
            Console.WriteLine($"I am a {(Thread.CurrentThread.IsThreadPoolThread ? "Thread Pool" : "Custom")} thread.");
        }
    }
}
