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
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(800, 450);
            Controls.Add(ReturnButton);
            Name = "ResultForm";
            Text = "ResultForm";
            ResumeLayout(false);
        }

        #endregion

        private Button ReturnButton;
    }
}