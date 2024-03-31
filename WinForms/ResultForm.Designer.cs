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
            SuspendLayout();
            // 
            // ReturnButton
            // 
            ReturnButton.Location = new Point(12, 27);
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
            SaveResultToFileButton.Location = new Point(334, 12);
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
            VisitedCounterTextBox.Location = new Point(12, 108);
            VisitedCounterTextBox.Name = "VisitedCounterTextBox";
            VisitedCounterTextBox.ReadOnly = true;
            VisitedCounterTextBox.Size = new Size(397, 20);
            VisitedCounterTextBox.TabIndex = 2;
            VisitedCounterTextBox.Text = "Кількість пройдених вершин під час алгоритму: ";
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(800, 450);
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
    }
}