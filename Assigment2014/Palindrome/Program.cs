using System;
using System.Text.RegularExpressions;

namespace Palindrome
{
    class Program
    {
        #region Main
        static void Main(string[] args)
        {
            Regex reg = new Regex("^[a-zA-Z]+$");
            Console.WriteLine("Please enter a string to see if it is a palindrome:");
            string word = Console.ReadLine().ToLower().Replace(" ", string.Empty);

            while (!reg.IsMatch(word))
            {
                Console.WriteLine("Invalid string!\nEnter new one: ");
                word = Console.ReadLine().ToLower().Replace(" ", string.Empty);
            }
            Console.WriteLine("String validated.");

            if (IsPalindrome(word)) Console.WriteLine("String \"{0}\" is palindrome!", word);
            else Console.WriteLine("String \"{0}\" is not palindrome...", word);

            Console.ReadKey();
        }
        #endregion

        #region IsPalindrome
        /// <summary>
        /// Check whether the string is palindrome or not
        /// </summary>
        /// <param name="s">Actual string</param>
        /// <returns></returns>
        static bool IsPalindrome(string s)
        {
            for (int i = 0; i < s.Length / 2; i++)
                if (s[i] != s[s.Length - 1 - i])
                    return false;
            return true;
        }
        #endregion
    }
}
