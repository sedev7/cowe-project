﻿namespace COWE.Client
{
    partial class Client
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.topPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottonPanel = new System.Windows.Forms.Panel();
            this.FlooderStatusIndicatorsGroupBox = new System.Windows.Forms.GroupBox();
            this.YellowLabel = new System.Windows.Forms.Label();
            this.RedLabel = new System.Windows.Forms.Label();
            this.GreenLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.ProgressSpinnerPictureBox = new System.Windows.Forms.PictureBox();
            this.clockLabel = new System.Windows.Forms.Label();
            this.AddDeleteFlooderGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteFlooderButton = new System.Windows.Forms.Button();
            this.AddFlooderButton = new System.Windows.Forms.Button();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.FlooderTimerIntervalGroupBox = new System.Windows.Forms.GroupBox();
            this.FlooderIntervalTextBox = new System.Windows.Forms.TextBox();
            this.FlooderTargetGroupBox = new System.Windows.Forms.GroupBox();
            this.TargetIpAddressTextBox = new System.Windows.Forms.TextBox();
            this.TargetPortTextBox = new System.Windows.Forms.TextBox();
            this.DatabaseResetGroupBox = new System.Windows.Forms.GroupBox();
            this.DatabaseResetCheckBox = new System.Windows.Forms.CheckBox();
            this.ParseCaptureFilesServiceGroupBox = new System.Windows.Forms.GroupBox();
            this.StartParseFilesServiceButton = new System.Windows.Forms.Button();
            this.ParseFilesServiceStatusLabel = new System.Windows.Forms.Label();
            this.ClockButton = new System.Windows.Forms.Button();
            this.FlooderStatusDataGridView = new System.Windows.Forms.DataGridView();
            this.SelectFlooder = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Pid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flooder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlooderTarget = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FlooderControl = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ClientTabControl = new System.Windows.Forms.TabControl();
            this.FlooderTabPage = new System.Windows.Forms.TabPage();
            this.AnalysisTabPage = new System.Windows.Forms.TabPage();
            this.AnalysisMainPanel = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.bottonPanel.SuspendLayout();
            this.FlooderStatusIndicatorsGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProgressSpinnerPictureBox)).BeginInit();
            this.AddDeleteFlooderGroupBox.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.FlooderTimerIntervalGroupBox.SuspendLayout();
            this.FlooderTargetGroupBox.SuspendLayout();
            this.DatabaseResetGroupBox.SuspendLayout();
            this.ParseCaptureFilesServiceGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlooderStatusDataGridView)).BeginInit();
            this.ClientTabControl.SuspendLayout();
            this.FlooderTabPage.SuspendLayout();
            this.AnalysisTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.SystemColors.Control;
            this.topPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.topPanel.Controls.Add(this.label1);
            this.topPanel.Controls.Add(this.menuStrip1);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1011, 138);
            this.topPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Virtual Machine Co-Residency Probe";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1009, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "MainMenuMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.logToolStripMenuItem.Text = "Log";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // bottonPanel
            // 
            this.bottonPanel.BackColor = System.Drawing.SystemColors.Control;
            this.bottonPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bottonPanel.Controls.Add(this.FlooderStatusIndicatorsGroupBox);
            this.bottonPanel.Controls.Add(this.groupBox1);
            this.bottonPanel.Controls.Add(this.AddDeleteFlooderGroupBox);
            this.bottonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottonPanel.Location = new System.Drawing.Point(0, 434);
            this.bottonPanel.Name = "bottonPanel";
            this.bottonPanel.Size = new System.Drawing.Size(994, 100);
            this.bottonPanel.TabIndex = 1;
            // 
            // FlooderStatusIndicatorsGroupBox
            // 
            this.FlooderStatusIndicatorsGroupBox.Controls.Add(this.YellowLabel);
            this.FlooderStatusIndicatorsGroupBox.Controls.Add(this.RedLabel);
            this.FlooderStatusIndicatorsGroupBox.Controls.Add(this.GreenLabel);
            this.FlooderStatusIndicatorsGroupBox.Location = new System.Drawing.Point(237, 5);
            this.FlooderStatusIndicatorsGroupBox.Name = "FlooderStatusIndicatorsGroupBox";
            this.FlooderStatusIndicatorsGroupBox.Size = new System.Drawing.Size(506, 82);
            this.FlooderStatusIndicatorsGroupBox.TabIndex = 9;
            this.FlooderStatusIndicatorsGroupBox.TabStop = false;
            // 
            // YellowLabel
            // 
            this.YellowLabel.BackColor = System.Drawing.Color.Khaki;
            this.YellowLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.YellowLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YellowLabel.Location = new System.Drawing.Point(193, 32);
            this.YellowLabel.Name = "YellowLabel";
            this.YellowLabel.Size = new System.Drawing.Size(121, 25);
            this.YellowLabel.TabIndex = 4;
            this.YellowLabel.Text = "Non-Resident";
            this.YellowLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RedLabel
            // 
            this.RedLabel.BackColor = System.Drawing.Color.LightCoral;
            this.RedLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RedLabel.Location = new System.Drawing.Point(349, 32);
            this.RedLabel.Name = "RedLabel";
            this.RedLabel.Size = new System.Drawing.Size(121, 25);
            this.RedLabel.TabIndex = 6;
            this.RedLabel.Text = "Not Connected";
            this.RedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GreenLabel
            // 
            this.GreenLabel.BackColor = System.Drawing.Color.LightGreen;
            this.GreenLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GreenLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GreenLabel.Location = new System.Drawing.Point(37, 32);
            this.GreenLabel.Name = "GreenLabel";
            this.GreenLabel.Size = new System.Drawing.Size(121, 25);
            this.GreenLabel.TabIndex = 2;
            this.GreenLabel.Text = "Co-Resident";
            this.GreenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ProgressLabel);
            this.groupBox1.Controls.Add(this.ProgressSpinnerPictureBox);
            this.groupBox1.Controls.Add(this.clockLabel);
            this.groupBox1.Location = new System.Drawing.Point(749, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 82);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgressLabel.Location = new System.Drawing.Point(54, 16);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(160, 58);
            this.ProgressLabel.TabIndex = 2;
            this.ProgressLabel.Text = "Progress Label";
            this.ProgressLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProgressSpinnerPictureBox
            // 
            this.ProgressSpinnerPictureBox.Image = global::COWE.Client.Properties.Resources.fedora_spinner;
            this.ProgressSpinnerPictureBox.InitialImage = null;
            this.ProgressSpinnerPictureBox.Location = new System.Drawing.Point(10, 29);
            this.ProgressSpinnerPictureBox.Name = "ProgressSpinnerPictureBox";
            this.ProgressSpinnerPictureBox.Size = new System.Drawing.Size(38, 31);
            this.ProgressSpinnerPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ProgressSpinnerPictureBox.TabIndex = 1;
            this.ProgressSpinnerPictureBox.TabStop = false;
            // 
            // clockLabel
            // 
            this.clockLabel.Location = new System.Drawing.Point(29, 26);
            this.clockLabel.Name = "clockLabel";
            this.clockLabel.Size = new System.Drawing.Size(172, 31);
            this.clockLabel.TabIndex = 0;
            this.clockLabel.Text = "Clock";
            this.clockLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddDeleteFlooderGroupBox
            // 
            this.AddDeleteFlooderGroupBox.Controls.Add(this.DeleteFlooderButton);
            this.AddDeleteFlooderGroupBox.Controls.Add(this.AddFlooderButton);
            this.AddDeleteFlooderGroupBox.Location = new System.Drawing.Point(11, 5);
            this.AddDeleteFlooderGroupBox.Name = "AddDeleteFlooderGroupBox";
            this.AddDeleteFlooderGroupBox.Size = new System.Drawing.Size(220, 82);
            this.AddDeleteFlooderGroupBox.TabIndex = 7;
            this.AddDeleteFlooderGroupBox.TabStop = false;
            // 
            // DeleteFlooderButton
            // 
            this.DeleteFlooderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteFlooderButton.Location = new System.Drawing.Point(39, 47);
            this.DeleteFlooderButton.Name = "DeleteFlooderButton";
            this.DeleteFlooderButton.Size = new System.Drawing.Size(139, 27);
            this.DeleteFlooderButton.TabIndex = 7;
            this.DeleteFlooderButton.Text = "Delete Flooder";
            this.DeleteFlooderButton.UseVisualStyleBackColor = true;
            this.DeleteFlooderButton.Click += new System.EventHandler(this.DeleteFlooderButton_Click);
            // 
            // AddFlooderButton
            // 
            this.AddFlooderButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddFlooderButton.Location = new System.Drawing.Point(39, 13);
            this.AddFlooderButton.Name = "AddFlooderButton";
            this.AddFlooderButton.Size = new System.Drawing.Size(139, 27);
            this.AddFlooderButton.TabIndex = 6;
            this.AddFlooderButton.Text = "Add Flooder";
            this.AddFlooderButton.UseVisualStyleBackColor = true;
            this.AddFlooderButton.Click += new System.EventHandler(this.AddFlooderButton_Click);
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.bottonPanel);
            this.mainPanel.Controls.Add(this.groupBox2);
            this.mainPanel.Controls.Add(this.ClockButton);
            this.mainPanel.Controls.Add(this.FlooderStatusDataGridView);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(3, 3);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(996, 536);
            this.mainPanel.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.FlooderTimerIntervalGroupBox);
            this.groupBox2.Controls.Add(this.FlooderTargetGroupBox);
            this.groupBox2.Controls.Add(this.DatabaseResetGroupBox);
            this.groupBox2.Controls.Add(this.ParseCaptureFilesServiceGroupBox);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(23, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(942, 95);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // FlooderTimerIntervalGroupBox
            // 
            this.FlooderTimerIntervalGroupBox.Controls.Add(this.FlooderIntervalTextBox);
            this.FlooderTimerIntervalGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlooderTimerIntervalGroupBox.Location = new System.Drawing.Point(275, 24);
            this.FlooderTimerIntervalGroupBox.Name = "FlooderTimerIntervalGroupBox";
            this.FlooderTimerIntervalGroupBox.Size = new System.Drawing.Size(132, 63);
            this.FlooderTimerIntervalGroupBox.TabIndex = 8;
            this.FlooderTimerIntervalGroupBox.TabStop = false;
            this.FlooderTimerIntervalGroupBox.Text = "Flooder Interval (sec)";
            // 
            // FlooderIntervalTextBox
            // 
            this.FlooderIntervalTextBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.FlooderIntervalTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlooderIntervalTextBox.Location = new System.Drawing.Point(31, 23);
            this.FlooderIntervalTextBox.Name = "FlooderIntervalTextBox";
            this.FlooderIntervalTextBox.Size = new System.Drawing.Size(66, 26);
            this.FlooderIntervalTextBox.TabIndex = 6;
            this.FlooderIntervalTextBox.Text = "120";
            this.FlooderIntervalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // FlooderTargetGroupBox
            // 
            this.FlooderTargetGroupBox.Controls.Add(this.TargetIpAddressTextBox);
            this.FlooderTargetGroupBox.Controls.Add(this.TargetPortTextBox);
            this.FlooderTargetGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlooderTargetGroupBox.Location = new System.Drawing.Point(23, 24);
            this.FlooderTargetGroupBox.Name = "FlooderTargetGroupBox";
            this.FlooderTargetGroupBox.Size = new System.Drawing.Size(246, 63);
            this.FlooderTargetGroupBox.TabIndex = 9;
            this.FlooderTargetGroupBox.TabStop = false;
            this.FlooderTargetGroupBox.Text = "Target (Server) - IP Address and Port Number";
            // 
            // TargetIpAddressTextBox
            // 
            this.TargetIpAddressTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetIpAddressTextBox.Location = new System.Drawing.Point(14, 23);
            this.TargetIpAddressTextBox.Name = "TargetIpAddressTextBox";
            this.TargetIpAddressTextBox.Size = new System.Drawing.Size(138, 26);
            this.TargetIpAddressTextBox.TabIndex = 2;
            this.TargetIpAddressTextBox.Text = "255.255.255.255";
            this.TargetIpAddressTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TargetPortTextBox
            // 
            this.TargetPortTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TargetPortTextBox.Location = new System.Drawing.Point(165, 23);
            this.TargetPortTextBox.Name = "TargetPortTextBox";
            this.TargetPortTextBox.Size = new System.Drawing.Size(66, 26);
            this.TargetPortTextBox.TabIndex = 3;
            this.TargetPortTextBox.Text = "65536";
            this.TargetPortTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DatabaseResetGroupBox
            // 
            this.DatabaseResetGroupBox.Controls.Add(this.DatabaseResetCheckBox);
            this.DatabaseResetGroupBox.Location = new System.Drawing.Point(413, 24);
            this.DatabaseResetGroupBox.Name = "DatabaseResetGroupBox";
            this.DatabaseResetGroupBox.Size = new System.Drawing.Size(234, 63);
            this.DatabaseResetGroupBox.TabIndex = 10;
            this.DatabaseResetGroupBox.TabStop = false;
            this.DatabaseResetGroupBox.Text = "Database Reset /  Purge Old Files";
            // 
            // DatabaseResetCheckBox
            // 
            this.DatabaseResetCheckBox.AutoSize = true;
            this.DatabaseResetCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatabaseResetCheckBox.Location = new System.Drawing.Point(9, 25);
            this.DatabaseResetCheckBox.Name = "DatabaseResetCheckBox";
            this.DatabaseResetCheckBox.Size = new System.Drawing.Size(213, 24);
            this.DatabaseResetCheckBox.TabIndex = 0;
            this.DatabaseResetCheckBox.Text = "Reset Database and Files";
            this.DatabaseResetCheckBox.UseVisualStyleBackColor = true;
            // 
            // ParseCaptureFilesServiceGroupBox
            // 
            this.ParseCaptureFilesServiceGroupBox.Controls.Add(this.StartParseFilesServiceButton);
            this.ParseCaptureFilesServiceGroupBox.Controls.Add(this.ParseFilesServiceStatusLabel);
            this.ParseCaptureFilesServiceGroupBox.Location = new System.Drawing.Point(653, 24);
            this.ParseCaptureFilesServiceGroupBox.Name = "ParseCaptureFilesServiceGroupBox";
            this.ParseCaptureFilesServiceGroupBox.Size = new System.Drawing.Size(266, 63);
            this.ParseCaptureFilesServiceGroupBox.TabIndex = 9;
            this.ParseCaptureFilesServiceGroupBox.TabStop = false;
            this.ParseCaptureFilesServiceGroupBox.Text = "Parse Capture Files Service";
            // 
            // StartParseFilesServiceButton
            // 
            this.StartParseFilesServiceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StartParseFilesServiceButton.Location = new System.Drawing.Point(22, 21);
            this.StartParseFilesServiceButton.Name = "StartParseFilesServiceButton";
            this.StartParseFilesServiceButton.Size = new System.Drawing.Size(85, 30);
            this.StartParseFilesServiceButton.TabIndex = 8;
            this.StartParseFilesServiceButton.Text = "Start";
            this.StartParseFilesServiceButton.UseVisualStyleBackColor = true;
            this.StartParseFilesServiceButton.Click += new System.EventHandler(this.StartParseFilesServiceButton_Click);
            // 
            // ParseFilesServiceStatusLabel
            // 
            this.ParseFilesServiceStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ParseFilesServiceStatusLabel.Location = new System.Drawing.Point(128, 25);
            this.ParseFilesServiceStatusLabel.Name = "ParseFilesServiceStatusLabel";
            this.ParseFilesServiceStatusLabel.Size = new System.Drawing.Size(100, 23);
            this.ParseFilesServiceStatusLabel.TabIndex = 0;
            this.ParseFilesServiceStatusLabel.Text = "Status";
            this.ParseFilesServiceStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClockButton
            // 
            this.ClockButton.Location = new System.Drawing.Point(902, 373);
            this.ClockButton.Name = "ClockButton";
            this.ClockButton.Size = new System.Drawing.Size(75, 23);
            this.ClockButton.TabIndex = 10;
            this.ClockButton.Text = "Start Clock";
            this.ClockButton.UseVisualStyleBackColor = true;
            this.ClockButton.Click += new System.EventHandler(this.ClockButton_Click);
            // 
            // FlooderStatusDataGridView
            // 
            this.FlooderStatusDataGridView.AllowUserToAddRows = false;
            this.FlooderStatusDataGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FlooderStatusDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.FlooderStatusDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FlooderStatusDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SelectFlooder,
            this.Pid,
            this.Flooder,
            this.Port,
            this.FlooderTarget,
            this.RunTime,
            this.Status,
            this.FlooderControl});
            this.FlooderStatusDataGridView.Location = new System.Drawing.Point(68, 116);
            this.FlooderStatusDataGridView.Name = "FlooderStatusDataGridView";
            this.FlooderStatusDataGridView.ReadOnly = true;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlooderStatusDataGridView.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.FlooderStatusDataGridView.Size = new System.Drawing.Size(887, 153);
            this.FlooderStatusDataGridView.TabIndex = 0;
            this.FlooderStatusDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FlooderStatusDataGridView_CellContentClick);
            // 
            // SelectFlooder
            // 
            this.SelectFlooder.HeaderText = "";
            this.SelectFlooder.MinimumWidth = 30;
            this.SelectFlooder.Name = "SelectFlooder";
            this.SelectFlooder.ReadOnly = true;
            this.SelectFlooder.Width = 30;
            // 
            // Pid
            // 
            this.Pid.HeaderText = "PID";
            this.Pid.MinimumWidth = 60;
            this.Pid.Name = "Pid";
            this.Pid.ReadOnly = true;
            this.Pid.Width = 60;
            // 
            // Flooder
            // 
            this.Flooder.HeaderText = "Flooder";
            this.Flooder.MinimumWidth = 150;
            this.Flooder.Name = "Flooder";
            this.Flooder.ReadOnly = true;
            this.Flooder.ToolTipText = "IP address";
            this.Flooder.Width = 150;
            // 
            // Port
            // 
            this.Port.HeaderText = "Port";
            this.Port.MinimumWidth = 80;
            this.Port.Name = "Port";
            this.Port.ReadOnly = true;
            this.Port.Width = 80;
            // 
            // FlooderTarget
            // 
            this.FlooderTarget.HeaderText = "Destination";
            this.FlooderTarget.MinimumWidth = 150;
            this.FlooderTarget.Name = "FlooderTarget";
            this.FlooderTarget.ReadOnly = true;
            this.FlooderTarget.Width = 150;
            // 
            // RunTime
            // 
            this.RunTime.HeaderText = "Run Time (sec)";
            this.RunTime.Name = "RunTime";
            this.RunTime.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 122;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Status.Width = 122;
            // 
            // FlooderControl
            // 
            this.FlooderControl.HeaderText = "";
            this.FlooderControl.MinimumWidth = 100;
            this.FlooderControl.Name = "FlooderControl";
            this.FlooderControl.ReadOnly = true;
            this.FlooderControl.Text = "Start";
            this.FlooderControl.UseColumnTextForButtonValue = true;
            // 
            // ClientTabControl
            // 
            this.ClientTabControl.Controls.Add(this.FlooderTabPage);
            this.ClientTabControl.Controls.Add(this.AnalysisTabPage);
            this.ClientTabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClientTabControl.Location = new System.Drawing.Point(1, 108);
            this.ClientTabControl.Name = "ClientTabControl";
            this.ClientTabControl.SelectedIndex = 0;
            this.ClientTabControl.Size = new System.Drawing.Size(1010, 571);
            this.ClientTabControl.TabIndex = 3;
            this.ClientTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.ClientTabControl_Selected);
            // 
            // FlooderTabPage
            // 
            this.FlooderTabPage.BackColor = System.Drawing.Color.White;
            this.FlooderTabPage.Controls.Add(this.mainPanel);
            this.FlooderTabPage.Location = new System.Drawing.Point(4, 25);
            this.FlooderTabPage.Name = "FlooderTabPage";
            this.FlooderTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.FlooderTabPage.Size = new System.Drawing.Size(1002, 542);
            this.FlooderTabPage.TabIndex = 0;
            this.FlooderTabPage.Text = "Flooder";
            // 
            // AnalysisTabPage
            // 
            this.AnalysisTabPage.Controls.Add(this.AnalysisMainPanel);
            this.AnalysisTabPage.Location = new System.Drawing.Point(4, 25);
            this.AnalysisTabPage.Name = "AnalysisTabPage";
            this.AnalysisTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AnalysisTabPage.Size = new System.Drawing.Size(1002, 542);
            this.AnalysisTabPage.TabIndex = 1;
            this.AnalysisTabPage.Text = "Analysis";
            this.AnalysisTabPage.UseVisualStyleBackColor = true;
            // 
            // AnalysisMainPanel
            // 
            this.AnalysisMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnalysisMainPanel.Location = new System.Drawing.Point(3, 3);
            this.AnalysisMainPanel.Name = "AnalysisMainPanel";
            this.AnalysisMainPanel.Size = new System.Drawing.Size(996, 536);
            this.AnalysisMainPanel.TabIndex = 0;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 680);
            this.Controls.Add(this.ClientTabControl);
            this.Controls.Add(this.topPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(1027, 718);
            this.MinimumSize = new System.Drawing.Size(1027, 718);
            this.Name = "Client";
            this.Text = "Co-Residency Probe (Client)";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.bottonPanel.ResumeLayout(false);
            this.FlooderStatusIndicatorsGroupBox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProgressSpinnerPictureBox)).EndInit();
            this.AddDeleteFlooderGroupBox.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.FlooderTimerIntervalGroupBox.ResumeLayout(false);
            this.FlooderTimerIntervalGroupBox.PerformLayout();
            this.FlooderTargetGroupBox.ResumeLayout(false);
            this.FlooderTargetGroupBox.PerformLayout();
            this.DatabaseResetGroupBox.ResumeLayout(false);
            this.DatabaseResetGroupBox.PerformLayout();
            this.ParseCaptureFilesServiceGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlooderStatusDataGridView)).EndInit();
            this.ClientTabControl.ResumeLayout(false);
            this.FlooderTabPage.ResumeLayout(false);
            this.AnalysisTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel bottonPanel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.DataGridView FlooderStatusDataGridView;
        private System.Windows.Forms.Label RedLabel;
        private System.Windows.Forms.Label YellowLabel;
        private System.Windows.Forms.Label GreenLabel;
        private System.Windows.Forms.GroupBox FlooderStatusIndicatorsGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox AddDeleteFlooderGroupBox;
        private System.Windows.Forms.Button DeleteFlooderButton;
        private System.Windows.Forms.Button AddFlooderButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.Label clockLabel;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SelectFlooder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flooder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewTextBoxColumn FlooderTarget;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewButtonColumn FlooderControl;
        private System.Windows.Forms.Button ClockButton;
        private System.Windows.Forms.PictureBox ProgressSpinnerPictureBox;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox FlooderTimerIntervalGroupBox;
        private System.Windows.Forms.TextBox FlooderIntervalTextBox;
        private System.Windows.Forms.Button StartParseFilesServiceButton;
        private System.Windows.Forms.GroupBox FlooderTargetGroupBox;
        private System.Windows.Forms.TextBox TargetIpAddressTextBox;
        private System.Windows.Forms.TextBox TargetPortTextBox;
        private System.Windows.Forms.GroupBox DatabaseResetGroupBox;
        private System.Windows.Forms.CheckBox DatabaseResetCheckBox;
        private System.Windows.Forms.GroupBox ParseCaptureFilesServiceGroupBox;
        private System.Windows.Forms.Label ParseFilesServiceStatusLabel;
        private System.Windows.Forms.TabControl ClientTabControl;
        private System.Windows.Forms.TabPage FlooderTabPage;
        private System.Windows.Forms.TabPage AnalysisTabPage;
        private System.Windows.Forms.Panel AnalysisMainPanel;
    }
}

