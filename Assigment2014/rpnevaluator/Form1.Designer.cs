namespace RPNEvaluator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.evaluate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.finalResult = new System.Windows.Forms.Label();
            this.RPNExpression = new System.Windows.Forms.TextBox();
            this.StackContentListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // evaluate
            // 
            this.evaluate.Location = new System.Drawing.Point(193, 111);
            this.evaluate.Name = "evaluate";
            this.evaluate.Size = new System.Drawing.Size(75, 23);
            this.evaluate.TabIndex = 0;
            this.evaluate.Text = "Evaluate";
            this.evaluate.UseVisualStyleBackColor = true;
            this.evaluate.Click += new System.EventHandler(this.evaluate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter RPN here";
            // 
            // finalResult
            // 
            this.finalResult.AutoSize = true;
            this.finalResult.Location = new System.Drawing.Point(190, 170);
            this.finalResult.Name = "finalResult";
            this.finalResult.Size = new System.Drawing.Size(0, 13);
            this.finalResult.TabIndex = 2;
            // 
            // RPNExpression
            // 
            this.RPNExpression.Location = new System.Drawing.Point(12, 45);
            this.RPNExpression.Name = "RPNExpression";
            this.RPNExpression.Size = new System.Drawing.Size(474, 20);
            this.RPNExpression.TabIndex = 0;
            // 
            // StackContentListBox
            // 
            this.StackContentListBox.FormattingEnabled = true;
            this.StackContentListBox.HorizontalScrollbar = true;
            this.StackContentListBox.Location = new System.Drawing.Point(514, 13);
            this.StackContentListBox.Name = "StackContentListBox";
            this.StackContentListBox.Size = new System.Drawing.Size(325, 368);
            this.StackContentListBox.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 258);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Supported operators are +, -, *, /, ^, %";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tips:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(70, 284);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(225, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Use space \' \'  to express a number (ex. 12 3 +)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(70, 311);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(291, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Use dot \'.\'  to express decimal numbers (ex. 1.23 2 +, .5 .5 +)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(70, 341);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(385, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Check list box on the right side to see the content of the stack at each operatio" +
    "n";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(70, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(305, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Invalid expressions will not be accepted(ex 22+, 2+2, 1, hello...)";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(851, 395);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StackContentListBox);
            this.Controls.Add(this.RPNExpression);
            this.Controls.Add(this.finalResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.evaluate);
            this.Name = "Form1";
            this.Text = "RPN Evaluator";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button evaluate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label finalResult;
        private System.Windows.Forms.TextBox RPNExpression;
        private System.Windows.Forms.ListBox StackContentListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

