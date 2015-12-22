using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RPNEvaluator
{
    public partial class Form1 : Form
    {
        #region Private
        /// <summary>
        /// List of all elements 
        /// </summary>
        private List<Element> elements;

        /// <summary>
        /// Regular expression, to check if RPN is in valid format
        /// a number, decimal or not, must be followed by a space, 
        /// number with multiple dots are not allowed (2.2.2), .5 is allowed = 0.5
        /// operators allowed are - * / + ^(exponential), %(modulus)
        /// other characters are not allowed
        /// </summary>
        private Regex regRpn = new Regex(@"\G((\d*(\.\d+)?[ ])+[-*/+^%])+$");

        /// <summary>
        /// Regular expression to convert all two or more spaces into one space ex: "   " -> " "
        /// </summary>
        private Regex spaceRemover = new Regex(@"[ ]{2,}", RegexOptions.None);
        private RPNEvaluator rpnEvaluator = new RPNEvaluator();

        private Parser parser = new Parser();
        #endregion

        #region Form1
        public Form1()
        {
            InitializeComponent();
        }
        #endregion

        #region evaluate_Click
        /// <summary>
        /// On button click event evaluate expression if valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void evaluate_Click(object sender, EventArgs e)
        {
            StackContentListBox.Items.Clear();
            errorProvider1.SetError(RPNExpression, "");
            RPNExpression.Text = spaceRemover.Replace(RPNExpression.Text.Trim(), @" ");

            if(!regRpn.IsMatch(RPNExpression.Text))
            {
                errorProvider1.SetError(RPNExpression, "Invalid expression.");
                return;
            }

            elements = new List<Element>();

            if((elements = parser.GetElementsFrom(RPNExpression.Text)) == null)
            {
                errorProvider1.SetError(RPNExpression, "Missing operator or operand.");
                return;
            }

            finalResult.Text = "Result = " + rpnEvaluator.GetResult(elements);
            PrintStackContent();
        }
        #endregion

        #region PrintStackContent
        /// <summary>
        /// Print all elements in stack
        /// </summary>
        private void PrintStackContent()
        {
            StackContentListBox.Items.Clear();

            foreach(string s in rpnEvaluator.stackContent)
                StackContentListBox.Items.Add(s);
        }
        #endregion
    }
}
