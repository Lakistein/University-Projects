namespace RPNEvaluator
{
    #region enum OperatorType
    /// <summary>
    /// Representing operators
    /// </summary>
    public enum OperatorType
    {
        ADD,
        SUB,
        DIV,
        MUL,
        POW,
        MOD,
    }
    #endregion

    class Operator : Element
    {
        #region Public
        /// <summary>
        /// OperatorType enum
        /// </summary>
        public OperatorType type;
        #endregion

        #region Private variables
        /// <summary>
        /// Char representing a operator 
        /// </summary>
        private char ch;
        #endregion

        #region Constructor
        /// <summary>
        /// Creating new operator object
        /// </summary>
        /// <param name="ch">Char representing a operator</param>
        public Operator(char ch)
        {
            this.ch = ch;
            switch(ch)
            {
                case '+': type = OperatorType.ADD; break;
                case '-': type = OperatorType.SUB; break;
                case '*': type = OperatorType.MUL; break;
                case '/': type = OperatorType.DIV; break;
                case '^': type = OperatorType.POW; break;
                case '%': type = OperatorType.MOD; break;
            }
        }
        #endregion

        #region ToString
        /// <summary>
        /// Convert operator to string
        /// </summary>
        /// <returns>Operator in string format</returns>
        public override string ToString()
        {
            return ch.ToString();
        }
        #endregion
    }
}
