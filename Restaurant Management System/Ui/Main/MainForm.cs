using Cafe_Management_System.Ui.Control;
using Cafe_Management_System.Ui.Event;
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
using System_Cafe_Manager.Witget;

namespace Cafe_Management_System
{
    public partial class MainForm : Form
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

        public IEventHandler FormClickEvent;
        private UserControl activeControl;
        public MainForm()
        {
            InitializeComponent();

            OpenControl(new HomeControl());
        }
        private void Search_click(object sender, EventArgs e)
        {
            string text = tbSearch.Text.ToString();
            FormClickEvent?.OnFormSearch(text);
        }

        private void OpenControl(UserControl control)
        {
            if (activeControl != null && activeControl.GetType() == control.GetType())
            {
                activeControl.BringToFront();
            }
            else
            {
                // Remove the existing control (if any)
                if (activeControl != null)
                    this.Controls.Remove(activeControl);

                // Set the new active control
                activeControl = control;

                // Dock and add the control
                control.Dock = DockStyle.Fill;
                try
                {
                    this.FormClickEvent = (control as IEventHandler);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error: ${e.Message}");
                }
                finally
                {
                    containerControl.Controls.Add(control);
                }

            }
        }

        private void pnTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ControlSnap(e);
        }

        private void icRestore_Click(object sender, EventArgs e)
        {
            ResizeForm();
        }

        private void icMiniForm_Click(object sender, EventArgs e)
        {
            MiniForm();
        }

        private void icCloseApp_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void pnNotification_Click(object sender, EventArgs e)
        {
            var listControl = new List<ControlItem>
            {
                new ControlItem { Text = "Item 1", OnClick = () => MessageBox.Show("Item 1 clicked!") },
                new ControlItem { Text = "Item 2", OnClick = () => MessageBox.Show("Item 2 clicked!") },
                new ControlItem { Text = "Item 3", OnClick = () => MessageBox.Show("Item 3 clicked!") }
            }; new MenuContextControl(listControl).ShowAt(pnNotification, this, false);

        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                string text = tbSearch.Text.ToString();
                FormClickEvent?.OnFormSearch(text);
            }
        }

        private void pnReloalControl_Click(object sender, EventArgs e)
        {
            FormClickEvent?.OnReloadData();
        }

        private void icHomeClick_Click(object sender, EventArgs e)
        {
            HomeControl h = new HomeControl();
            if(activeControl !=null && activeControl.GetType() !=h.GetType())
            {
                OpenControl(h);
            }
        }
    }
}
