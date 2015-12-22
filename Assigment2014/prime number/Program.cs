using System;
using System.Diagnostics;
namespace Prime_Number
{
    class Program
    {
        #region GetInput
        public static int GetInput()
        {
            int limit;
            int.TryParse(Console.ReadLine(), out limit);

            if(limit <= 0)
            {
                Console.WriteLine("Invalid number\nApplication will terminate ...");
                Console.ReadLine();
                Environment.Exit(0);
            }
            return limit;
        }
        #endregion

        #region IsPrime
        public static bool IsPrime(int num)
        {
            if(num == 2 || num == 3) return true;
            if(num % 2 == 0 || num % 3 == 0) return false;

            int i = 5, w = 2;

            while(i * i <= num)
            {
                if(num % i == 0)
                    return false;
                i += w;
                w = 6 - w;
            }
            return true;
        }
        #endregion

        #region Main
        static void Main(string[] args)
        {
            #region Console Modification
            Console.Title = "Prime number generator";
            Console.BufferHeight = Int16.MaxValue - 1;
            Console.WindowWidth = Console.LargestWindowWidth / 2;
            Console.WindowHeight = Console.LargestWindowHeight / 2;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Clear();
            #endregion

            #region Get Input and check if prime
            Console.Write("Enter a number to check whether it is a prime or not: ");
            int numm = GetInput();

            if(IsPrime(numm)) Console.WriteLine("It is a prime");
            else Console.WriteLine("It is not a prime");
            #endregion

            #region Get Input and create new objects
            Console.Write("Enter a limit up to which to search for prime numbers: ");
            int limit = GetInput();

            Stopwatch sw = new Stopwatch();
            Sieve_Of_Lazar SoL = new Sieve_Of_Lazar(limit);
            Sieve_of_Atkin SoA = new Sieve_of_Atkin(limit);
            Sieve_of_Eratosthenes SoE = new Sieve_of_Eratosthenes(limit);
            #endregion

            #region Search by Sieve of Lazar
            Console.Write("Searching primes with Sieve of Lazar ...");
            sw.Start();
            SoL.FindAllPrimesTillLimit();
            sw.Stop();
            Console.WriteLine("\rSieve of Lazar found {0} prime number{1} in:\t\t" + sw.Elapsed, SoL.NumberOfPrimes.ToString("N0"), (SoL.NumberOfPrimes > 1) ? "s" : "");
            sw.Reset();
            #endregion

            #region Search by Sieve of Atkin
            Console.Write("Searching primes with Sieve of Atkin ...");
            sw.Start();
            SoA.FindAllPrimesTillLimit();
            sw.Stop();
            Console.WriteLine("\rSieve of Atkin found {0} prime number{1} in:\t\t" + sw.Elapsed, SoA.NumberOfPrimes.ToString("N0"), (SoA.NumberOfPrimes > 1) ? "s" : "");
            sw.Reset();
            #endregion

            #region Search by Sieve of Erathostenes
            Console.Write("Searching primes with Sieve of Erathostenes ...");
            sw.Start();
            SoE.FindAllPrimesTillLimit();
            sw.Stop();
            Console.WriteLine("\rSieve of Erathostenes found {0} prime number{1} in:\t" + sw.Elapsed + Environment.NewLine, SoE.NumberOfPrimes.ToString("N0"), (SoE.NumberOfPrimes > 1) ? "s" : "");
            #endregion

            #region Check if number is prime
            Console.Write("Enter a number to check whether it is a prime or not: ");
            int num = GetInput();
            if(num <= limit)
                if(SoA.IsPrime(num))
                    Console.WriteLine("Number is prime.");
                else Console.WriteLine("Number is not prime.");
            else Console.WriteLine("Invalid number.");
            #endregion

            #region Print primes
            Console.Write("Print elements? [y/n] ");
            if(Console.Read() == 'y')
            {
                sw.Reset();
                sw.Start();
                SoA.PrintPrimes();
                sw.Stop();
                Console.WriteLine(Environment.NewLine + "Time spent printing: " + sw.Elapsed);
                Console.WindowTop = 0;
            }
            else Console.WriteLine("Thank you. bye");
            #endregion

            Console.ReadKey();
        }
        #endregion
    }
}
