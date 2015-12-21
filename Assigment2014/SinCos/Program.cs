using System;

namespace SinCos
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            double angle;
            string ch;
            do
            {
                Console.Write("Please enter the angle in degrees: ");
                if(double.TryParse(Console.ReadLine(), out angle)) {
                    Console.WriteLine("Sin of {0} is " + Sin(angle, 20), angle);
                    Console.WriteLine("Cos of {0} is " + Cos(angle, 20), angle);
                }
                else Console.WriteLine("Invalid number");

                Console.Write("Do you want to calculate another sin/cos?[y/n] ");
                ch = Console.ReadLine().ToLower();
            } while (ch[0] != 'n');
        }
        #endregion

        #region Factorial
        /// <summary>
        /// Calculate factorial of a number
        /// </summary>
        /// <param name="x">Actual number</param>
        /// <returns>Calculated factorial</returns>
        public static double Fact(double x)
        {
            return (x <= 1 ? 1 : x * Fact(x - 1));
        }
        #endregion

        #region Sin
        /// <summary>
        /// Calculate the Sine of given number
        /// </summary>
        /// <param name="x">Actual number</param>
        /// <param name="n">Number of iterations</param>
        /// <returns>Sine of specified angle</returns>
        public static double Sin(double x, int n)
        {
            double result = 0;
            x *= (Math.PI / 180); // Converting degrees to radians
            
            for (byte i = 0; i < n; i++)
                result += (-(i & 1) | 1) * (Math.Pow(x, 2 * i + 1) / Fact(2 * i + 1));

            return Math.Round(result, 10);
        }
        #endregion

        #region Cos
        /// <summary>
        /// Calculate the cosine of given number
        /// </summary>
        /// <param name="x">Actual number</param>
        /// <param name="n">Number of iterations</param>
        /// <returns>Cosine of specified angle</returns>
        public static double Cos(double x, int n)
        {
            double result = 0;
            x *= (Math.PI / 180); //Converting degrees to radians

            for (byte i = 0; i < n; i++)
                result += (-(i & 1) | 1) * (Math.Pow(x, 2 * i) / Fact(2 * i));

            return Math.Round(result, 10);
        }
        #endregion
    }
}
