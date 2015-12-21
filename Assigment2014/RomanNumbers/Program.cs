using System;

namespace RomanNumbers
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            int num;
      
            do {
                Console.Write("Insert value betwheen 1 and 3999: ");
                int.TryParse(Console.ReadLine(), out num);
            } while (num <= 0 || num > 3999);

            Console.WriteLine("Number {0} is in roman representation: " + ArabToRoman(num), num);

            Console.ReadKey();
        }
        #endregion

        #region ArabToRoman
        public static string ArabToRoman(int num)
        {
            if (num >= 1000) return "M" + ArabToRoman(num - 1000);
            if (num >= 900) return "CM" + ArabToRoman(num - 900);
            if (num >= 500) return "D" + ArabToRoman(num - 500);
            if (num >= 400) return "CD" + ArabToRoman(num - 400);
            if (num >= 100) return "C" + ArabToRoman(num - 100);
            if (num >= 90) return "XC" + ArabToRoman(num - 90);
            if (num >= 50) return "L" + ArabToRoman(num - 50);
            if (num >= 40) return "XL" + ArabToRoman(num - 40);
            if (num >= 10) return "X" + ArabToRoman(num - 10);
            if (num >= 9) return "IX" + ArabToRoman(num - 9);
            if (num >= 5) return "V" + ArabToRoman(num - 5);
            if (num >= 4) return "IV" + ArabToRoman(num - 4);
            if (num >= 1) return "I" + ArabToRoman(num - 1);
            if (num < 1) return string.Empty;
            return string.Empty;
        }
        #endregion
    }
}
