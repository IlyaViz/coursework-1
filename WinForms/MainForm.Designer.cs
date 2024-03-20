namespace WinForms
{
    partial class MainForm
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
            MazeLengthTextBox = new TextBox();
            MainTextBox = new TextBox();
            MazeLengthLabel = new Label();
            MethodListBox = new ListBox();
            label1 = new Label();
            RandomMazeButton = new Button();
            FindButton = new Button();
            SuspendLayout();
            // 
            // MazeLengthTextBox
            // 
            MazeLengthTextBox.Location = new Point(321, 99);
            MazeLengthTextBox.Name = "MazeLengthTextBox";
            MazeLengthTextBox.Size = new Size(137, 27);
            MazeLengthTextBox.TabIndex = 0;
            MazeLengthTextBox.TextChanged += MazeLengthTextBox_TextChanged;
            // 
            // MainTextBox
            // 
            MainTextBox.BackColor = SystemColors.Control;
            MainTextBox.BorderStyle = BorderStyle.None;
            MainTextBox.Font = new Font("Showcard Gothic", 28.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            MainTextBox.Location = new Point(93, -5);
            MainTextBox.Name = "MainTextBox";
            MainTextBox.Size = new Size(653, 59);
            MainTextBox.TabIndex = 1;
            MainTextBox.Text = "Пошук шляхів в лабиринті";
            // 
            // MazeLengthLabel
            // 
            MazeLengthLabel.AutoSize = true;
            MazeLengthLabel.Location = new Point(278, 76);
            MazeLengthLabel.Name = "MazeLengthLabel";
            MazeLengthLabel.Size = new Size(225, 20);
            MazeLengthLabel.TabIndex = 2;
            MazeLengthLabel.Text = "Введіть розмірність лабиринту";
            // 
            // MethodListBox
            // 
            MethodListBox.FormattingEnabled = true;
            MethodListBox.Items.AddRange(new object[] { "Метод Дейкстри", "Метод A*" });
            MethodListBox.Location = new Point(321, 183);
            MethodListBox.Name = "MethodListBox";
            MethodListBox.Size = new Size(137, 44);
            MethodListBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(300, 160);
            label1.Name = "label1";
            label1.Size = new Size(186, 20);
            label1.TabIndex = 4;
            label1.Text = "Виберіть метод розв'язку";
            // 
            // RandomMazeButton
            // 
            RandomMazeButton.BackColor = SystemColors.ButtonFace;
            RandomMazeButton.Enabled = false;
            RandomMazeButton.ForeColor = SystemColors.InfoText;
            RandomMazeButton.Location = new Point(529, 81);
            RandomMazeButton.Name = "RandomMazeButton";
            RandomMazeButton.Size = new Size(114, 63);
            RandomMazeButton.TabIndex = 5;
            RandomMazeButton.Text = "Випадковий лабіринт";
            RandomMazeButton.UseVisualStyleBackColor = false;
            RandomMazeButton.Click += RandomMazeButton_Click;
            // 
            // FindButton
            // 
            FindButton.BackColor = SystemColors.ButtonFace;
            FindButton.Enabled = false;
            FindButton.ForeColor = SystemColors.InfoText;
            FindButton.Location = new Point(333, 250);
            FindButton.Name = "FindButton";
            FindButton.Size = new Size(113, 40);
            FindButton.TabIndex = 6;
            FindButton.Text = "Знайти";
            FindButton.UseVisualStyleBackColor = false;
            FindButton.Click += FindButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(800, 450);
            Controls.Add(FindButton);
            Controls.Add(RandomMazeButton);
            Controls.Add(label1);
            Controls.Add(MethodListBox);
            Controls.Add(MazeLengthLabel);
            Controls.Add(MainTextBox);
            Controls.Add(MazeLengthTextBox);
            Name = "MainForm";
            Text = "MainForm";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox MazeLengthTextBox;
        private TextBox MainTextBox;
        private Label MazeLengthLabel;
        private ListBox MethodListBox;
        private Label label1;
        private Button RandomMazeButton;
        private Button FindButton;
    }
}
