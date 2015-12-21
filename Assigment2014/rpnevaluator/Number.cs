using System;

namespace RPNEvaluator
{
    class Number : Element
    {
        #region Private Variables
        /// <summary>
        /// Value of the number
        /// </summary>
        private double value;
        #endregion

        #region Public Properties
        public double Value
        {
            get { return value; }
        }
        #endregion

        #region Constructor Number
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="number">String representing a number</param>
        public Number(string number)
        {
            double.TryParse(number, out this.value);
        }
        #endregion

        #region Operator ^ overloading
        /// <summary>
        /// Operator overloading ^
        /// </summary>
        /// <param name="n1">First number</param>
        /// <param name="n2">Second number (exponential)</param>
        /// <returns>Result of Math.pow</returns>
        public static double operator ^(Number n1, Number n2)
        {
            return Math.Pow(n1.Value, n2.Value);
        }
        #endregion

        #region ToString
        /// <summary>
        /// ToString() override
        /// </summary>
        /// <returns>Value of number in string format</returns>
        public override string ToString()
        {
            return value.ToString();
        }
        #endregion
    }
}
