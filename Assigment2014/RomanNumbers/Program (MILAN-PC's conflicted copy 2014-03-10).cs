using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string num = Console.ReadLine();


            Console.WriteLine("Number {0} is in roman representation: " + RomanNumber(num), num);

            Console.ReadKey();

        }

        public static string RomanNumber(string num)
        {
            string romanNumber = "";
            int digit;

            for (int i = num.Length; i > 0; i--)
            {
                digit = ;
                switch (i)
                {
                    
                    case 4:
                        break;
                    case 3:
                        break;
                    case 2:
                        break;
                    case 1:
                        if (digit == 1)
                            romanNumber += "I";
                        else if (digit == 2)
                            romanNumber += "II";
                        else if (digit == 3)
                            romanNumber += "III";
                        else if (digit == 4)
                            romanNumber += "IV";
                        else if (digit == 5)
                            romanNumber += "V";
                        else if (digit == 6)
                            romanNumber += "VI";
                        else if (digit == 7)
                            romanNumber += "VI";
                        else if (digit == 8)
                            romanNumber += "VII";
                        else if (digit == 9)
                            romanNumber += "IX";
                        break;
                }
            }
            return romanNumber;

        }
    }
}
