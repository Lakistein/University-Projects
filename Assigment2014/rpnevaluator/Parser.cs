using System;
using System.Collections.Generic;

namespace RPNEvaluator
{
    class Parser
    {
        #region Private
        /// <summary>
        /// List of all elements parsed from a string (numbers and operators)
        /// </summary>
        public static List<Element> elements;
        #endregion

        #region Parse
        /// <summary>
        /// Parse string and store all elements (number and operators) to the list
        /// </summary>
        /// <param name="expression">String to be parsed</param>
        private void Parse(string expression)
        {
            for(int i = 0; i < expression.Length; i++)
            {
                if(Char.IsNumber(expression[i]) || expression[i] == '.')
                    elements.Add(new Number(GetNumberAt(expression, ref i)));
                else if(IsOperator(expression[i]))
                    elements.Add(new Operator(expression[i]));
            }
        }
        #endregion

        #region GetElementsFrom
        /// <summary>
        /// Gets all elements from string format
        /// </summary>
        /// <param name="RPNexpression"></param>
        /// <returns></returns>
        public List<Element> GetElementsFrom(string RPNexpression)
        {
            elements = new List<Element>();
            Parse(RPNexpression);

            return this.IsValid() ? elements : null;
        }
        #endregion

        #region isValid
        /// <summary>
        /// Check if expression is valid
        /// count of numbers must be for 1 greater than count of operators
        /// </summary>
        /// <returns>True if expression is valid, otherwise false</returns>
        private bool IsValid()
        {
            int num = 0, op = 0;

            foreach(Element element in elements)
            {
                if(element is Number)
                    num++;
                else op++;
            }
            return (num - op == 1);
        }
        #endregion

        #region isOperator
        /// <summary>
        /// Check if the character is valid operator
        /// </summary>
        /// <param name="ch"></param>
        /// <returns>True if it is operator</returns>
        private bool IsOperator(char ch)
        {
            return (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '^' || ch == '%');
        }
        #endregion

        #region GetNumber
        /// <summary>
        /// Checks if number is multiple digit number or decimal
        /// if not it will return the first character of a string(signle digit number)
        /// </summary>
        /// <param name="expr">String expression where number is</param>
        /// <param name="pos">The first index of a number in string</param>
        /// <returns>String of a number. (one digit, multiple digit or decimal number)</returns>
        private string GetNumberAt(string expr, ref int pos)
        {
            string num;
            int index = pos;

            while(expr[index] != ' ')
                index++;

            num = expr.Substring(pos, index - pos);
            pos = index;

            return num;
        }
        #endregion
    }
}
