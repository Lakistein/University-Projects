using System;
using System.Collections.Generic;

namespace RPNEvaluator
{
    class RPNEvaluator
    {
        #region Private
        /// <summary>
        /// Stack where will be stored numbers, and popped later for calculation purposes
        /// </summary>
        private Stack<Number> stack = new Stack<Number>();
        #endregion

        #region Public
        /// <summary>
        /// List of strings that holds all operations done on stack, 
        /// such as added new element, all current elements on the stack after new one is added or removed
        /// </summary>
        public List<string> stackContent = new List<string>();
        #endregion

        #region Calculate
        /// <summary>
        /// Method calculating two numbers
        /// </summary>
        /// <param name="first">First operand</param>
        /// <param name="op">Operator</param>
        /// <param name="second">Second operand</param>
        /// <returns>Calculated value of two operands</returns>
        private Number Calculate(Number first, Operator op, Number second)
        {
            double result = 0;

            switch(op.type)
            {
                case OperatorType.ADD: result = first.Value + second.Value; break;
                case OperatorType.SUB: result = first.Value - second.Value; break;
                case OperatorType.MUL: result = first.Value * second.Value; break;
                case OperatorType.DIV: result = first.Value / second.Value; break;
                case OperatorType.POW: result = first ^ second; break;
                case OperatorType.MOD: result = first.Value % second.Value; break;
            }

            return new Number(Convert.ToString(result));
        }
        #endregion

        #region GetResult
        /// <summary>
        /// Method that chech each element in the list, if it is number push it on the stack, 
        /// if it is operator pop two numbers from the stack and do some operation on them
        /// </summary>
        /// <param name="elements">List of elements previously parsed</param>
        /// <returns>Final value</returns>
        public double GetResult(List<Element> elements)
        {
            double result = 0;
            stackContent.Clear();

            for(int i = 0; i < elements.Count; i++)
            {
                if(elements[i] is Number)
                {
                    stack.Push(elements[i] as Number);
                    stackContent.Add("Number " + elements[i].ToString() + " added.");
                }
                else
                {
                    Number second = stack.Pop(), first = stack.Pop();
                    stackContent.Add("Performing " + elements[i].ToString() + " operation on " + first.ToString() + " and " + second.ToString());
                    stack.Push(Calculate(first, elements[i] as Operator, second));
                }
                TraverseStack();
            }
            return result = stack.Pop().Value;
        }
        #endregion

        #region TraverseStack
        /// <summary>
        /// Traverses the stack and add content to the stack for printing
        /// </summary>
        private void TraverseStack()
        {
            stackContent.Add("Current content of the stack is:");
            foreach(Number n in stack)
                stackContent.Add("\t\t " + n.ToString());

            stackContent.Add("-------------------------------------------------------------------------------------------------------------");
        }
        #endregion
    }
}
