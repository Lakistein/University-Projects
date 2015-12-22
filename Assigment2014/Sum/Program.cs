using System;

namespace Sum
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            int lowerLimit, higherLimit;

            GetInput(out lowerLimit, out higherLimit);

            Console.WriteLine("Result of sum iteratively from {0} to {1} is {2}", lowerLimit, higherLimit, SumIter(lowerLimit, higherLimit));

            Console.WriteLine("Result of sum recursively from {0} to {1} is {2}", lowerLimit, higherLimit, SumRec(lowerLimit, higherLimit));

            Console.ReadLine();

        }
        #endregion

        #region SumRec
        /// <summary>
        /// Sumnation of all odd numbers between 2 odd numbers recuresevly
        /// </summary>
        /// <param name="lowerLimit">Lower limit of odd number</param>
        /// <param name="higherLimit">Upper limit of odd number</param>
        /// <returns>Sum of numbers</returns>
        public static int SumRec(int lowerLimit, int higherLimit)
        {
            if(lowerLimit > higherLimit)
                return 0;

            return lowerLimit + SumRec(lowerLimit + 2, higherLimit);
        }
        #endregion

        #region SumIter
        /// <summary>
        /// Sumnation of all odd numbers between 2 odd numbers iteratevly
        /// </summary>
        /// <param name="lowerLimit">Lower limit of odd number</param>
        /// <param name="higherLimit">Upper limit of odd number</param>
        /// <returns>Sum of numbers</returns>
        public static int SumIter(int lowerLimit, int higherLimit)
        {
            int result = 0;

            for(int i = lowerLimit; i <= higherLimit; i += 2) 
                result += i;

            return result;
        }
        #endregion

        #region GetInput
        /// <summary>
        /// Get input with input validation, can return only 2 odd number where first is smaller than the second
        /// </summary>
        /// <param name="lowerLimit">Lower limit input</param>
        /// <param name="higherLimit">Upper limit input</param>
        public static void GetInput(out int lowerLimit, out int higherLimit)
        {
            do {
                Console.Write("Please enter the lower limit odd number: ");
                if(!int.TryParse(Console.ReadLine(), out lowerLimit))
                    continue;
            } while(lowerLimit % 2 == 0);

            do {
                Console.Write("Please enter the higher limit odd number(bigger than {0}): ", lowerLimit);
                if(!int.TryParse(Console.ReadLine(), out higherLimit))
                    continue;
            } while(higherLimit % 2 == 0 || higherLimit <= lowerLimit);
        }
        #endregion

    }
}
