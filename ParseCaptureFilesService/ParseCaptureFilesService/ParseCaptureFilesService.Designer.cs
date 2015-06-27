namespace COWE.ParseCaptureFilesService
{
    partial class ParseCaptureFilesService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._FileSystemWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this._FileSystemWatcher)).BeginInit();
            // 
            // _FileSystemWatcher
            // 
            this._FileSystemWatcher.EnableRaisingEvents = true;
            // 
            // ParseCaptureFilesService
            // 
            this.ServiceName = "ParseCaptureFilesService";
            ((System.ComponentModel.ISupportInitialize)(this._FileSystemWatcher)).EndInit();

        }

        #endregion

        private System.IO.FileSystemWatcher _FileSystemWatcher;
    }
}
