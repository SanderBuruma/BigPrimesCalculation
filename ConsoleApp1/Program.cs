using System;
using System.Collections.Generic;
using System.Numerics;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// A program which should spit out reall big primes.
        /// </summary>
        /// <param name="args"></param>
        static void Main()
        {
            Random random = new Random();

            while (true)
            {
                BigInteger newInt = GenerateRandomBigInt(random);
            }
        }

        static private BigInteger GenerateRandomBigInt(Random random)
        {
            byte[] bytes = new byte[(int)Config.PrimeSizeInBytes];
            random.NextBytes(bytes);
            bytes[(int)Config.PrimeSizeInBytes - 1] = byte.Parse("0");

            return new BigInteger(bytes);
        }
    }

    enum Config
    {
        PrimeSizeInBytes = 65
    }
}
