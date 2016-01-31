using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COWE.Client
{
    public partial class NotifyUserForm : Form
    {
        static private string _DisplayText = string.Empty;
        static private Point location = new Point(10,10);

        // Delegate for cross thead call to close form
        private delegate void CloseDelegate();

        // Type of form to be displayed
        private static NotifyUserForm noticeForm;
        public NotifyUserForm()
        {
            InitializeComponent();
        }

        static public void ShowRecalculatingNotice(string displayText, Point coordinates)
        {
            location = coordinates;
            _DisplayText = displayText;
            Thread t = new Thread(new ThreadStart(NotifyUserForm.ShowForm));
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }
        private void RecalculatingNoticeForm_Load(object sender, EventArgs e)
        {
            pictureBox1.BringToFront();
            pictureBox1.Image = Properties.Resources.fedora_spinner;
            pictureBox1.Focus();
            Application.DoEvents();
        }

        static private void ShowForm()
        {
            noticeForm = new NotifyUserForm();
            noticeForm.DisplayTextLabel.Text = _DisplayText;
            noticeForm.BackColor = Color.LightSteelBlue;
            noticeForm.Location = location;
            noticeForm.StartPosition = FormStartPosition.Manual;
            //noticeForm.StartPosition = FormStartPosition.CenterParent;
            Application.Run(noticeForm);
        }

        static public void CloseForm()
        {
            noticeForm.Invoke(new CloseDelegate(NotifyUserForm.CloseFormInternal));
            
        }

        static private void CloseFormInternal()
        {
            noticeForm.Close();
        }
    }
}
