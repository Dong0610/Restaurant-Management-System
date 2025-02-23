using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Management_System.Ui.Main
{
    public partial class HomeForm : Form
    {

        #region Code Form
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_SIZEBOX = 0x00040000; // Enables window resizing
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_SIZEBOX; // Ensure window remains resizable
                return cp;
            }
        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_NCACTIVATE = 0x0086;
            const int WM_NCHITTEST = 0x0084;

            if (m.Msg == WM_NCCALCSIZE)
            {
                // Prevents extra space while keeping resizing active
                m.Result = IntPtr.Zero;
                return;
            }

            if (m.Msg == WM_NCACTIVATE)
            {
                // Prevents Windows from repainting the non-client area (fixes title bar issue)
                m.Result = new IntPtr(-1);
                return;
            }
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                int borderSize = 20; // Invisible border size for resizing
                Point cursorPosition = PointToClient(Cursor.Position);

                Rectangle leftEdge = new Rectangle(0, 0, borderSize, this.ClientSize.Height);
                Rectangle rightEdge = new Rectangle(this.ClientSize.Width - borderSize, 0, borderSize, this.ClientSize.Height);
                Rectangle topEdge = new Rectangle(0, 0, this.ClientSize.Width, borderSize);
                Rectangle bottomEdge = new Rectangle(0, this.ClientSize.Height - borderSize, this.ClientSize.Width, borderSize);

                if (leftEdge.Contains(cursorPosition)) m.Result = (IntPtr)10;  // HTLEFT
                else if (rightEdge.Contains(cursorPosition)) m.Result = (IntPtr)11; // HTRIGHT
                else if (topEdge.Contains(cursorPosition)) m.Result = (IntPtr)12;   // HTTOP
                else if (bottomEdge.Contains(cursorPosition)) m.Result = (IntPtr)15; // HTBOTTOM
            }
        }
        [DllImport("user32.dll")]
        private static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        public void CloseForm()
        {
            this.Hide();

            this.Close();
            Application.Exit();
        }

        public void MiniForm()
        {
            this.WindowState = FormWindowState.Minimized;
        }

        public void ResizeForm()
        {

            this.WindowState = (this.WindowState == FormWindowState.Maximized) ? FormWindowState.Normal : FormWindowState.Maximized;
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.Padding = new Padding(4, 4, 8, 8);
            }
            else
            {
                this.Padding = new Padding(4, 4, 20, 20);
            }
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
        public HomeForm()
        {
            InitializeComponent();
        }

        private void pnTitle_MouseDown(object sender, MouseEventArgs e)
        {
            this.Opacity =95;
            ControlSnap(e);
        }

        private void pnTitle_MouseUp(object sender, MouseEventArgs e)
        {
            this.Opacity = 100;
        }
    }
}
