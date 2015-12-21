using System;

namespace SquareRootAproximation
{
    class Program
    {
        static void Main(string[] args)
        {
            double guess, value, epsilon = 1.0e-9;

            Console.Write("Enter a number: ");
            if(double.TryParse(Console.ReadLine(), out value)) {
                Console.Write("Enter a guess: ");
                if(double.TryParse(Console.ReadLine(), out guess)) {
                    double result = ((value / guess) + guess) / 2;

                    while(Math.Abs(result - guess) > epsilon) {
                        guess = result;
                        result = ((value / guess) + guess) / 2;
                    }
                    Console.WriteLine("The approx sqrt of {0} is {1}", value, result);
                }
                else Console.WriteLine("Invalid Number");
            }
            else Console.WriteLine("Invalid Number");

            Console.ReadKey();
        }
    }
}
