namespace Restaurant_Management_System.Ui.Main
{
    partial class HomeForm
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
            this.components = new System.ComponentModel.Container();
            this.HoverForm = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.pnTitle = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.pnMenu = new Guna.UI2.WinForms.Guna2GradientPanel();
            this.guna2ContainerControl1 = new Guna.UI2.WinForms.Guna2ContainerControl();
            this.SuspendLayout();
            // 
            // HoverForm
            // 
            this.HoverForm.AnimationInterval = 100;
            this.HoverForm.BorderRadius = 32;
            this.HoverForm.ContainerControl = this;
            this.HoverForm.DockIndicatorTransparencyValue = 0.6D;
            this.HoverForm.ShadowColor = System.Drawing.Color.Blue;
            this.HoverForm.TransparentWhileDrag = true;
            // 
            // pnTitle
            // 
            this.pnTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnTitle.CustomBorderColor = System.Drawing.Color.WhiteSmoke;
            this.pnTitle.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 1);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Location = new System.Drawing.Point(2, 2);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(1336, 52);
            this.pnTitle.TabIndex = 1;
            this.pnTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnTitle_MouseDown);
            this.pnTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnTitle_MouseUp);
            // 
            // pnMenu
            // 
            this.pnMenu.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnMenu.CustomBorderColor = System.Drawing.Color.WhiteSmoke;
            this.pnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnMenu.Location = new System.Drawing.Point(2, 54);
            this.pnMenu.Name = "pnMenu";
            this.pnMenu.Size = new System.Drawing.Size(240, 774);
            this.pnMenu.TabIndex = 3;
            // 
            // guna2ContainerControl1
            // 
            this.guna2ContainerControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.guna2ContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2ContainerControl1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.guna2ContainerControl1.Location = new System.Drawing.Point(242, 54);
            this.guna2ContainerControl1.Margin = new System.Windows.Forms.Padding(0);
            this.guna2ContainerControl1.Name = "guna2ContainerControl1";
            this.guna2ContainerControl1.Size = new System.Drawing.Size(1096, 774);
            this.guna2ContainerControl1.TabIndex = 4;
            this.guna2ContainerControl1.Text = "guna2ContainerControl1";
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1340, 830);
            this.Controls.Add(this.guna2ContainerControl1);
            this.Controls.Add(this.pnMenu);
            this.Controls.Add(this.pnTitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Blue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "HomeForm";
            this.Opacity = 0.99D;
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HomeForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2BorderlessForm HoverForm;
        private Guna.UI2.WinForms.Guna2GradientPanel pnTitle;
        private Guna.UI2.WinForms.Guna2GradientPanel pnMenu;
        private Guna.UI2.WinForms.Guna2ContainerControl guna2ContainerControl1;
    }
}