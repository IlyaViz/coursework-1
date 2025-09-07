namespace WinForms
{
    partial class ResultForm
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
            ReturnButton = new Button();
            SaveResultToFileButton = new Button();
            VisitedCounterTextBox = new TextBox();
            TakenTimeTextBox = new TextBox();
            PathLengthTextBox = new TextBox();
            MethodTextBox = new TextBox();
            SuspendLayout();
            // 
            // ReturnButton
            // 
            ReturnButton.Location = new Point(12, 31);
            ReturnButton.Name = "ReturnButton";
            ReturnButton.Size = new Size(119, 29);
            ReturnButton.TabIndex = 0;
            ReturnButton.Text = "Повернутися";
            ReturnButton.UseVisualStyleBackColor = true;
            ReturnButton.Click += ReturnButton_Click;
            // 
            // SaveResultToFileButton
            // 
            SaveResultToFileButton.Enabled = false;
            SaveResultToFileButton.Location = new Point(175, 12);
            SaveResultToFileButton.Name = "SaveResultToFileButton";
            SaveResultToFileButton.Size = new Size(140, 66);
            SaveResultToFileButton.TabIndex = 1;
            SaveResultToFileButton.Text = "Зберегти результат в файл";
            SaveResultToFileButton.UseVisualStyleBackColor = true;
            SaveResultToFileButton.Click += SaveResultToFileButton_Click;
            // 
            // VisitedCounterTextBox
            // 
            VisitedCounterTextBox.BorderStyle = BorderStyle.None;
            VisitedCounterTextBox.Location = new Point(12, 218);
            VisitedCounterTextBox.Name = "VisitedCounterTextBox";
            VisitedCounterTextBox.ReadOnly = true;
            VisitedCounterTextBox.Size = new Size(397, 20);
            VisitedCounterTextBox.TabIndex = 2;
            VisitedCounterTextBox.Text = "Кількість відвіданих вершин під час алгоритму: ";
            // 
            // TakenTimeTextBox
            // 
            TakenTimeTextBox.BorderStyle = BorderStyle.None;
            TakenTimeTextBox.Location = new Point(12, 166);
            TakenTimeTextBox.Name = "TakenTimeTextBox";
            TakenTimeTextBox.ReadOnly = true;
            TakenTimeTextBox.Size = new Size(397, 20);
            TakenTimeTextBox.TabIndex = 3;
            TakenTimeTextBox.Text = "Затрачений час: ";
            // 
            // PathLengthTextBox
            // 
            PathLengthTextBox.BorderStyle = BorderStyle.None;
            PathLengthTextBox.Location = new Point(12, 192);
            PathLengthTextBox.Name = "PathLengthTextBox";
            PathLengthTextBox.ReadOnly = true;
            PathLengthTextBox.Size = new Size(397, 20);
            PathLengthTextBox.TabIndex = 5;
            PathLengthTextBox.Text = "Довжина шляху: ";
            // 
            // MethodTextBox
            // 
            MethodTextBox.BackColor = SystemColors.Control;
            MethodTextBox.BorderStyle = BorderStyle.None;
            MethodTextBox.Font = new Font("Noto Sans Hebrew", 13.7999992F);
            MethodTextBox.Location = new Point(12, 128);
            MethodTextBox.Name = "MethodTextBox";
            MethodTextBox.Size = new Size(604, 32);
            MethodTextBox.TabIndex = 6;
            MethodTextBox.Text = "Метод: ";
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(800, 450);
            Controls.Add(MethodTextBox);
            Controls.Add(PathLengthTextBox);
            Controls.Add(TakenTimeTextBox);
            Controls.Add(VisitedCounterTextBox);
            Controls.Add(SaveResultToFileButton);
            Controls.Add(ReturnButton);
            Name = "ResultForm";
            Text = "ResultForm";
            WindowState = FormWindowState.Maximized;
            Load += ResultForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button ReturnButton;
        private Button SaveResultToFileButton;
        private TextBox VisitedCounterTextBox;
        private TextBox TakenTimeTextBox;
        private TextBox PathLengthTextBox;
        private TextBox MethodTextBox;
    }
}