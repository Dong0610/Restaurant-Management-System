using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace System_Cafe_Manager.Widget
{
    public enum DialogType
    {
        YesNo,
        Default,
        Message
    }

    public partial class AppDialog : Form
    {
        public delegate void DoAction(bool action);
        private DoAction actionCallback;

        public AppDialog(string message = "Message", string title = "Message", DialogType type = DialogType.Message)
        {
            InitializeComponent();
            txtTitle.Text = title;
            txtMessage.Text = message;

            // Set button visibility and text based on dialog type
            switch (type)
            {
                case DialogType.Default:
                    btnCancel.Text = "Cancel";
                    btnOk.Text = "OK";
                    btnYes.Visible = true;
                    btnCancel.Visible = true;
                    btnOk.Visible = false;
                    break;

                case DialogType.Message:
                    btnCancel.Visible = false;
                    btnOk.Text = "OK";
                    btnOk.Visible = true;
                    btnYes.Visible = false;
                    break;

                case DialogType.YesNo:
                    btnCancel.Text = "No";
                    btnYes.Text = "Yes";
                    btnYes.Visible = true;
                    btnOk.Visible = false;
                    btnCancel.Visible = true;
                    break;
            }

            // Display the dialog as modal
        }


        // Method to show dialog with custom behavior
        public  void ShowDialogResult( DoAction doAction = null)
        {
            actionCallback = doAction;
            this.ShowDialog();
        }

        // OK button click handler
        private void btnOk_Click(object sender, EventArgs e)
        {
            actionCallback?.Invoke(true); // User confirmed action
            CloseDialog();
        }

        // Yes button click handler
        private void btnYes_Click(object sender, EventArgs e)
        {
            actionCallback?.Invoke(true); // User clicked Yes
            CloseDialog();
        }

        // Cancel/No button click handler
        private void btnCancel_Click(object sender, EventArgs e)
        {
            actionCallback?.Invoke(false); // User clicked No or Cancel
            CloseDialog();
        }

        // Method to close the dialog properly
      
        private void AppDialog_MouseDown(object sender, MouseEventArgs e)
        {
            ControlSnap(e);
        }

        #region Code Form
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private const int GWL_STYLE = -16;
     
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        public void CloseDialog()
        {
            this.Hide();
            this.Close();            
        }
        public void ControlSnap(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion
    }
}











