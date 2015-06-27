namespace ParseCaptureFile
{
    partial class MainForm
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
            this.GetFileButton = new System.Windows.Forms.Button();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.GetFileOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.ParseFileButton = new System.Windows.Forms.Button();
            this.OutputRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ProcessPacketsButton = new System.Windows.Forms.Button();
            this.SaveProcessedFileButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // GetFileButton
            // 
            this.GetFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetFileButton.Location = new System.Drawing.Point(576, 64);
            this.GetFileButton.Name = "GetFileButton";
            this.GetFileButton.Size = new System.Drawing.Size(105, 36);
            this.GetFileButton.TabIndex = 0;
            this.GetFileButton.Text = "Get File";
            this.GetFileButton.UseVisualStyleBackColor = true;
            this.GetFileButton.Click += new System.EventHandler(this.GetFileButton_Click);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilePathTextBox.Location = new System.Drawing.Point(26, 72);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(523, 24);
            this.FilePathTextBox.TabIndex = 1;
            // 
            // GetFileOpenFileDialog
            // 
            this.GetFileOpenFileDialog.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Co-Residency Research Project - Parse a CSV Capture File";
            // 
            // ParseFileButton
            // 
            this.ParseFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParseFileButton.Location = new System.Drawing.Point(576, 127);
            this.ParseFileButton.Name = "ParseFileButton";
            this.ParseFileButton.Size = new System.Drawing.Size(105, 36);
            this.ParseFileButton.TabIndex = 3;
            this.ParseFileButton.Text = "Parse File";
            this.ParseFileButton.UseVisualStyleBackColor = true;
            this.ParseFileButton.Click += new System.EventHandler(this.ParseFileButton_Click);
            // 
            // OutputRichTextBox
            // 
            this.OutputRichTextBox.Location = new System.Drawing.Point(26, 127);
            this.OutputRichTextBox.Name = "OutputRichTextBox";
            this.OutputRichTextBox.Size = new System.Drawing.Size(523, 311);
            this.OutputRichTextBox.TabIndex = 4;
            this.OutputRichTextBox.Text = "";
            // 
            // ProcessPacketsButton
            // 
            this.ProcessPacketsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProcessPacketsButton.Location = new System.Drawing.Point(576, 190);
            this.ProcessPacketsButton.Name = "ProcessPacketsButton";
            this.ProcessPacketsButton.Size = new System.Drawing.Size(105, 52);
            this.ProcessPacketsButton.TabIndex = 5;
            this.ProcessPacketsButton.Text = "Process Packets";
            this.ProcessPacketsButton.UseVisualStyleBackColor = true;
            this.ProcessPacketsButton.Click += new System.EventHandler(this.ProcessPacketsButton_Click);
            // 
            // SaveProcessedFileButton
            // 
            this.SaveProcessedFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveProcessedFileButton.Location = new System.Drawing.Point(576, 274);
            this.SaveProcessedFileButton.Name = "SaveProcessedFileButton";
            this.SaveProcessedFileButton.Size = new System.Drawing.Size(105, 71);
            this.SaveProcessedFileButton.TabIndex = 6;
            this.SaveProcessedFileButton.Text = "Save Processed File";
            this.SaveProcessedFileButton.UseVisualStyleBackColor = true;
            this.SaveProcessedFileButton.Click += new System.EventHandler(this.SaveProcessedFileButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(576, 402);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(105, 36);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 473);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.SaveProcessedFileButton);
            this.Controls.Add(this.ProcessPacketsButton);
            this.Controls.Add(this.OutputRichTextBox);
            this.Controls.Add(this.ParseFileButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.GetFileButton);
            this.Name = "MainForm";
            this.Text = "Parse CSV Capture File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GetFileButton;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.OpenFileDialog GetFileOpenFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ParseFileButton;
        private System.Windows.Forms.RichTextBox OutputRichTextBox;
        private System.Windows.Forms.Button ProcessPacketsButton;
        private System.Windows.Forms.Button SaveProcessedFileButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
    }
}

