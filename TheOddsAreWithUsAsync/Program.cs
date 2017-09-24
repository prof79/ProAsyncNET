// Program.cs

namespace ProAsyncNet.TheOddsAreWithUsAsync
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;
    using System.Text;
    using System.Threading.Tasks;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            // ACME Lottery - The Odds Are With Us
            // The chances of guessing 600 numbers out of a pool of 49,000 numbers.
            BigInteger n = 49_000;
            BigInteger r = 600;

            Console.WriteLine();
            Console.WriteLine("*** ACME Lottery - The Odds Are With Us ***");
            Console.WriteLine();

            Console.WriteLine("Calculating ...");

            // Calculation 1
            var part1 = Task.Factory.StartNew(
                () => Factorial(n));

            // Calculation 2
            var part2 = Task.Factory.StartNew(
                () => Factorial(n - r));

            // Calculation 2
            var part3 = Task.Factory.StartNew(
                () => Factorial(r));

            // Final calculation (synchronous)
            var chances = part1.Result / (part2.Result * part3.Result);

            Console.WriteLine();
            Console.WriteLine($"Chances to guess {r} numbers out of {n:#,###0}: 1 : {chances:#,##0}");
            Console.WriteLine();
        }

        /// <summary>
        /// A quick-and dirty mathematical factorial function for
        /// n! = (n - 1) * (n - 2) ...
        /// </summary>
        /// <param name="n">
        /// The number to compute the factorial for.
        /// </param>
        /// <returns>
        /// The factorial of n as a <see cref="BigInteger"/>.
        /// </returns>
        private static BigInteger Factorial(BigInteger n)
        {
            if (n < 2)
            {
                return 1;
            }

            BigInteger factorial = 1;

            for (var i = n; i > 2; --i)
            {
                factorial *= i;
            }

            return factorial;
        }
    }
}
