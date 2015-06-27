namespace COWE.ParseCaptureFilesTestApp
{
    partial class ParseCaptureFilesTestForm
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
            this.ParseFilesButton = new System.Windows.Forms.Button();
            this.ParseFilesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ParseFilesButton
            // 
            this.ParseFilesButton.Location = new System.Drawing.Point(40, 48);
            this.ParseFilesButton.Name = "ParseFilesButton";
            this.ParseFilesButton.Size = new System.Drawing.Size(84, 27);
            this.ParseFilesButton.TabIndex = 0;
            this.ParseFilesButton.Text = "Parse Files";
            this.ParseFilesButton.UseVisualStyleBackColor = true;
            this.ParseFilesButton.Click += new System.EventHandler(this.ParseFilesButton_Click);
            // 
            // ParseFilesLabel
            // 
            this.ParseFilesLabel.AutoSize = true;
            this.ParseFilesLabel.Location = new System.Drawing.Point(37, 106);
            this.ParseFilesLabel.Name = "ParseFilesLabel";
            this.ParseFilesLabel.Size = new System.Drawing.Size(81, 13);
            this.ParseFilesLabel.TabIndex = 1;
            this.ParseFilesLabel.Text = "ParseFilesLabel";
            // 
            // ParseCaptureFilesTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 274);
            this.Controls.Add(this.ParseFilesLabel);
            this.Controls.Add(this.ParseFilesButton);
            this.Name = "ParseCaptureFilesTestForm";
            this.Text = "Parse Capture Files - Test Application";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ParseFilesButton;
        private System.Windows.Forms.Label ParseFilesLabel;
    }
}

