using Cafe_Management_System.Ui.Control;
using Cafe_Management_System.Ui.Event;
using FontAwesome.Sharp;
using Guna.UI2.WinForms;
using Restaurant_Management_System.Ui.Control;
using Restaurant_Management_System.Witget;
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

        public IEventHandler FormClickEvent;
        private UserControl activeControl;
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
        #endregion

        #region Menu App
        private bool isHidenMenu = true;
        private Timer menuAnimationTimer;
        private int targetWidth;
        private int animationStep;
        private int animationDuration = 150;
        private int frameRate = 15; // Frame rate in milliseconds
        private void icMenu_Click(object sender, EventArgs e)
        {
            int startWidth = pnMenu.Width;
            targetWidth = isHidenMenu ? 64 : 220; // Toggle target width
            int totalFrames = animationDuration / frameRate;
            animationStep = (targetWidth - startWidth) / totalFrames;
            if (menuAnimationTimer == null)
            {
                menuAnimationTimer = new Timer();
                menuAnimationTimer.Interval = frameRate; // Update every frame
                menuAnimationTimer.Tick += MenuAnimationTimer_Tick;
            }
            menuAnimationTimer.Start();
            isHidenMenu = !isHidenMenu;

        }
       

        private void MenuAnimationTimer_Tick(object sender, EventArgs e)
        {
            // Smoothly adjust the width
            pnMenu.Width += animationStep;

            // Check if the animation is complete
            if ((animationStep > 0 && pnMenu.Width >= targetWidth) ||
                (animationStep < 0 && pnMenu.Width <= targetWidth))
            {
                pnMenu.Width = targetWidth; // Ensure exact final width
                menuAnimationTimer.Stop();
            }
        }
        #endregion


        public HomeForm()
        {
            InitializeComponent();
            currentButonActive = icHome;
            LoopControl(pnMenu.Controls);
        
            OpenControl(new HomeControl());
           
        }

        private void LoopControl(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (var item in controls)
            {
                if (item is Guna2Panel guna2Panel)
                {
                    LoopControl(guna2Panel.Controls); // Recursively loop through the panel's controls
                }
                else if (item is Guna2GradientPanel gradientPanel)
                {
                    LoopControl(gradientPanel.Controls); // Recursively loop through the gradient panel's controls
                }
                else if (item is MenuButton iconButton)
                {
                  if(iconButton.Name== currentButonActive.Name)
                    {
                        iconButton.IsFocused = true;
                    }
                  else
                    {
                        iconButton.IsFocused = false;
                    }
                }
            }
        }

        private void MenuIcon_MouseHover(object sender, EventArgs e)
        {
            (sender as IconButton).BackColor = Color.FromArgb(220, 244, 255);
        }
        private void MenuIcon_MouseLeave(object sender, EventArgs e)
        {
            (sender as IconButton).BackColor = Color.Transparent;
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

        private void HomeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Q)
            {
                Application.Exit(); // Quit the application
            }
            else if (e.Control && e.KeyCode == Keys.F)
            {
                ResizeForm();
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                MiniForm();
            }
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            OpenControl(new SettingControl());
        }

        private void Close_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void icReload_Click(object sender, EventArgs e)
        {
            this?.FormClickEvent?.OnReloadData();
        }

        private void icSearch_Click(object sender, EventArgs e)
        {
            this.FormClickEvent?.OnFormSearch(tbSearch.Text.ToString());
        }

        private MenuButton currentButonActive=null;
      

      
        private void btnSignOut_Click(object sender, EventArgs e)
        {
           
            CloseForm();
        }

        private void icHome_Click(object sender, EventArgs e)
        {
            currentButonActive = (sender as MenuButton);
            LoopControl(pnMenu.Controls);
            OpenControl(new HomeControl());

        }
    }
}
