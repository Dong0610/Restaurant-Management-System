using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace System_Cafe_Manager.Witget
{
    public partial class MenuContextControl : Form
    {
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private FlowLayoutPanel flowLayoutPanel1;
        private System.ComponentModel.IContainer components;

        public MenuContextControl(List<ControlItem> listControl)
        {
            InitializeComponent();

            this.Load += MenuContextControl_Load;
            this.FormClosed += MenuContextControl_FormClosed;
            if (listControl.Count > 0)
            {
                listControl.ForEach((ControlItem item) =>
                {
                    Label lb = new Label
                    {
                        Text = item.Text,
                        Width = 224,
                        Height = 30,
                        Font = new Font("Arial", 10, FontStyle.Regular), // Correct way to set font
                        TextAlign = ContentAlignment.MiddleLeft, // Optional: Align text
                        Padding = new Padding(5) // Optional: Add some padding
                    };

                    lb.Click +=(s,e)=> { item.OnClick(); };

                    lb.MouseHover += Lb_MouseHover;
                    lb.MouseLeave += Lb_MouseLeave;
                    flowLayoutPanel1.Controls.Add(lb);
                });
            }

        }

        private void Lb_Click(object sender, EventArgs e)
        {
        }

        private void Lb_MouseLeave(object sender, EventArgs e)
        {
            var lb = (Label)sender;
            lb.BackColor = Color.Transparent;
            lb.ForeColor = Color.Black;
        }

        private void Lb_MouseHover(object sender, EventArgs e)
        {
            var lb = (Label)sender;
            lb.BackColor = Color.Blue;
            lb.ForeColor = Color.White;
        }

        private void MenuContextControl_Load(object sender, EventArgs e)
        {
            HookManager.AddMouseClickHandler(OnGlobalMouseClick);
        }

        private void MenuContextControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            HookManager.RemoveMouseClickHandler(OnGlobalMouseClick);
        }

        private void OnGlobalMouseClick(object sender, MouseEventArgs e)
        {
            // Close menu if clicked outside
            if (!this.Bounds.Contains(Cursor.Position))
            {
                
                this.Hide(); // Hide instead of close to reuse the form
               
            }
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.BorderRadius = 15;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(226, 126);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // MenuContextControl
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(250, 150);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuContextControl";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ResumeLayout(false);

        }

        public void ShowAt(Control control, Form form, bool showMenuUnderCursor)
        {
            if (this.IsDisposed)
                return;

            Point menuLocation;
            Rectangle formBounds = form.Bounds;
            Point controlScreenPos = control.PointToScreen(Point.Empty);

            int menuWidth = Math.Max(this.Width, 250);
            int menuHeight = Math.Max(this.Height, 350);

            if (showMenuUnderCursor)
            {
                Point screenPosition = Cursor.Position;

                if (screenPosition.X + menuWidth > formBounds.Right)
                    screenPosition.X = formBounds.Right - menuWidth;

                if (screenPosition.X < formBounds.Left)
                    screenPosition.X = formBounds.Left;

                if (screenPosition.Y + menuHeight > formBounds.Bottom)
                    screenPosition.Y = formBounds.Bottom - menuHeight;

                if (screenPosition.Y < formBounds.Top)
                    screenPosition.Y = formBounds.Top;

                menuLocation = screenPosition;
            }
            else
            {
                menuLocation = new Point(controlScreenPos.X, controlScreenPos.Y + control.Height+5);

                if (menuLocation.X + menuWidth > formBounds.Right)
                    menuLocation.X = formBounds.Right - menuWidth - 20;

                if (menuLocation.Y + menuHeight > formBounds.Bottom)
                    menuLocation.Y = controlScreenPos.Y - menuHeight;
            }

            this.Location = menuLocation;

            if (!this.Visible)
                this.Show();
            else
                this.BringToFront();
        }
    }

    // Global Mouse Hook Manager
    public static class HookManager
    {
        private static LowLevelMouseProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        private static event EventHandler<MouseEventArgs> MouseClick;

        public static void AddMouseClickHandler(EventHandler<MouseEventArgs> handler)
        {
            MouseClick += handler;
            if (_hookID == IntPtr.Zero)
            {
                _hookID = SetHook(_proc);
            }
        }

        public static void RemoveMouseClickHandler(EventHandler<MouseEventArgs> handler)
        {
            MouseClick -= handler;
            if (MouseClick == null && _hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
            }
        }

        private static IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (var curProcess = System.Diagnostics.Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                var hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                MouseClick?.Invoke(null, new MouseEventArgs(MouseButtons.Left, 1, hookStruct.pt.x, hookStruct.pt.y, 0));
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
