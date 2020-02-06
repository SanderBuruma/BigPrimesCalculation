using System;
using System.Diagnostics;
using System.Numerics;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// The configuration Enum
        /// </summary>
        enum Config
        {
            /// <summary>
            /// How many bytes to let the BigIntegers be
            /// </summary>
            MaxPrimeSizeInBytes = 100_000,
            MinPrimeSizeInBytes = 16,
        }
        /// <summary>
        /// A program which should spit out really big primes.
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            Random random = new Random();
            int bytesSize;
            Console.WriteLine("Please input the number of bytes to used by each individual prime (between {1} - {0}):", ((int)Config.MaxPrimeSizeInBytes).ToString("N0"), ((int)Config.MinPrimeSizeInBytes).ToString("N0"));
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out bytesSize))
                {
                    Console.WriteLine("Please input a valid number: ");
                }
                else if (bytesSize > (int)Config.MaxPrimeSizeInBytes)
                {
                    Console.WriteLine("Please input a number that's less than {0}", ((int)Config.MaxPrimeSizeInBytes).ToString("N0"));
                }
                else if (bytesSize < (int)Config.MinPrimeSizeInBytes)
                {
                    Console.WriteLine("Please input a number that's greater than {0}", ((int)Config.MinPrimeSizeInBytes).ToString("N0"));
                }
                else
                {
                    break;
                }
            }
            Console.Clear();

            BigInteger numberToCheck;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (true)
            {
                notPrimeB:;
                numberToCheck = GenerateRandomBigInt(random, bytesSize);
                foreach (int prime in new int[] { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 91, 97, 101 })
                {
                    //Simple relatively inexpensive check used to exclude some easy pickouts
                    if (numberToCheck % prime == 0) goto notPrimeB;
                }
                for (int j = 9; j < 11; j++)
                {
                    //Fermat test
                    if (BigInteger.ModPow(j, numberToCheck - 1, numberToCheck) != 1)
                    {
                        goto notPrimeB;
                    }
                }
                sw.Stop();
                Console.Clear();
                Console.WriteLine(numberToCheck.ToString("N0") + " is prime");
                Console.WriteLine(sw.ElapsedMilliseconds.ToString("N0") + " ms used\n");
                Console.WriteLine("press key to continue");
                Console.ReadKey();
                Console.WriteLine("----------------------calculating prime----------------------");
                sw.Restart();
            }
        }
        /// <summary>
        /// Generates only posiive bigints
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        static private BigInteger GenerateRandomBigInt(Random random, int bytesSize)
        {
            byte[] bytes = new byte[bytesSize+1];
            random.NextBytes(bytes);
            bytes[bytesSize] = byte.Parse("0");

            return new BigInteger(bytes);
        }
    }
}
