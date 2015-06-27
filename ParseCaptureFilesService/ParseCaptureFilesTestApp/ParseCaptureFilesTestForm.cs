using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using COWE.ParseCaptureFilesService;

namespace COWE.ParseCaptureFilesTestApp
{
    public partial class ParseCaptureFilesTestForm : Form
    {
        public ParseCaptureFilesTestForm()
        {
            InitializeComponent();
        }
       
        private void ParseFilesButton_Click(object sender, EventArgs e)
        {
            ParseFilesLabel.Text = "Parsing capture files...";

            ParseCaptureFilesService.ParseCaptureFilesService pcfs = new ParseCaptureFilesService.ParseCaptureFilesService();
            if(!pcfs.CheckForExistingFiles("C:\\temp\\CaptureFiles"))
            {
                ParseFilesLabel.Text = "Error parsing capture files!";
            }
            else
            {
                ParseFilesLabel.Text = "Capture files successfully parsed!";
            }
        }

    }
}
