namespace lab3
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            comboBoxInNS = new ComboBox();
            comboBoxOperation = new ComboBox();
            textBoxInput1 = new TextBox();
            textBoxInput2 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            buttonCalculate = new Button();
            textBoxOutput = new TextBox();
            textBoxOutputDC = new TextBox();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // comboBoxInNS
            // 
            comboBoxInNS.FormattingEnabled = true;
            comboBoxInNS.Items.AddRange(new object[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" });
            comboBoxInNS.Location = new Point(97, 12);
            comboBoxInNS.Name = "comboBoxInNS";
            comboBoxInNS.Size = new Size(109, 23);
            comboBoxInNS.TabIndex = 1;
            comboBoxInNS.Text = "10";
            // 
            // comboBoxOperation
            // 
            comboBoxOperation.FormattingEnabled = true;
            comboBoxOperation.Items.AddRange(new object[] { "+", "-", "*", "/" });
            comboBoxOperation.Location = new Point(97, 41);
            comboBoxOperation.Name = "comboBoxOperation";
            comboBoxOperation.Size = new Size(109, 23);
            comboBoxOperation.TabIndex = 0;
            comboBoxOperation.Text = "+";
            // 
            // textBoxInput1
            // 
            textBoxInput1.Location = new Point(97, 70);
            textBoxInput1.MaxLength = 11;
            textBoxInput1.Name = "textBoxInput1";
            textBoxInput1.Size = new Size(261, 23);
            textBoxInput1.TabIndex = 2;
            // 
            // textBoxInput2
            // 
            textBoxInput2.Location = new Point(97, 99);
            textBoxInput2.MaxLength = 11;
            textBoxInput2.Name = "textBoxInput2";
            textBoxInput2.Size = new Size(261, 23);
            textBoxInput2.TabIndex = 11;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 15);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 3;
            label1.Text = "Исходная СС";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 4;
            label2.Text = "Операция";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 73);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 5;
            label3.Text = "1 число";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 131);
            label4.Name = "label4";
            label4.Size = new Size(42, 15);
            label4.TabIndex = 6;
            label4.Text = "Вывод";
            // 
            // buttonCalculate
            // 
            buttonCalculate.Location = new Point(97, 186);
            buttonCalculate.Name = "buttonCalculate";
            buttonCalculate.Size = new Size(75, 23);
            buttonCalculate.TabIndex = 8;
            buttonCalculate.Text = "Перевести";
            buttonCalculate.UseVisualStyleBackColor = true;
            buttonCalculate.Click += ButtonCalculate_Click;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(97, 128);
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.Size = new Size(261, 23);
            textBoxOutput.TabIndex = 7;
            // 
            // textBoxOutputDC
            // 
            textBoxOutputDC.Location = new Point(97, 157);
            textBoxOutputDC.Name = "textBoxOutputDC";
            textBoxOutputDC.Size = new Size(261, 23);
            textBoxOutputDC.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 160);
            label5.Name = "label5";
            label5.Size = new Size(23, 15);
            label5.TabIndex = 10;
            label5.Text = "ПК";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 102);
            label6.Name = "label6";
            label6.Size = new Size(50, 15);
            label6.TabIndex = 12;
            label6.Text = "2 число";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(385, 219);
            Controls.Add(label6);
            Controls.Add(textBoxInput2);
            Controls.Add(label5);
            Controls.Add(textBoxOutputDC);
            Controls.Add(buttonCalculate);
            Controls.Add(textBoxOutput);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxInput1);
            Controls.Add(comboBoxInNS);
            Controls.Add(comboBoxOperation);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxInNS;
        private ComboBox comboBoxOperation;
        private TextBox textBoxInput1;
        private TextBox textBoxInput2;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBoxOutput;
        private Button buttonCalculate;
        private TextBox textBoxOutputDC;
        private Label label5;
        private Label label6;
    }
}
