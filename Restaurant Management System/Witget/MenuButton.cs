using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace Restaurant_Management_System.Witget
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using FontAwesome.Sharp;
    public enum MenuButtonGravity
    {
        Center,
        CenterHorizontal,
        CenterVertical,
        Start,
        End
    }

    public partial class MenuButton : UserControl
    {
        private IconButton icon;
        private Label textLabel;
        private IconButton endIcon;  // New end icon
        private MenuButtonGravity gravity = MenuButtonGravity.Center;
        private int iconMarginStart = 10;
        private bool isFocused = false;

        public MenuButton()
        {
            InitializeComponents();
            ApplyFocusStyle();
        }
        private void InitializeComponents()
        {
            // Set size and background
            this.Size = new Size(150, 40);
            this.BackColor = Color.Transparent;
            this.Padding = new Padding(5);
            this.BorderStyle = BorderStyle.None;

            // TableLayoutPanel for better alignment
            var layout = new TableLayoutPanel
            {
                ColumnCount = 3,
                Dock = DockStyle.Fill
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // Icon
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100)); // Text
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize)); // End Icon
            this.Controls.Add(layout);

            // Start Icon
            icon = new IconButton
            {
                IconChar = IconChar.Users,
                IconColor = Color.Black,
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat
            };
            icon.FlatAppearance.BorderSize = 0;
            icon.FlatAppearance.MouseOverBackColor = Color.Transparent;
            icon.FlatAppearance.MouseDownBackColor = Color.Transparent;
            layout.Controls.Add(icon, 0, 0); // Add to first column

            // Text Label
            textLabel = new Label
            {
                Text = "Click me",
                Font = new Font("Arial", 11, FontStyle.Regular),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            layout.Controls.Add(textLabel, 1, 0); // Add to second column

            // End Icon
            endIcon = new IconButton
            {
                IconChar = IconChar.None,
                IconColor = Color.Black,
                Size = new Size(24, 24),
                Margin = new Padding(0,8,16,0),
                IconSize = 20,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat
            };
            endIcon.FlatAppearance.BorderSize = 0;
            endIcon.FlatAppearance.MouseOverBackColor = Color.Transparent;
            endIcon.FlatAppearance.MouseDownBackColor = Color.Transparent;
            layout.Controls.Add(endIcon, 2, 0); // Add to third column

            // Hover Events
            this.MouseEnter += MenuButton_MouseEnter;
            this.MouseLeave += MenuButton_MouseLeave;
            textLabel.MouseEnter += MenuButton_MouseEnter;
            textLabel.MouseLeave += MenuButton_MouseLeave;
            icon.MouseEnter += MenuButton_MouseEnter;
            icon.MouseLeave += MenuButton_MouseLeave;
            endIcon.MouseEnter += MenuButton_MouseEnter;
            endIcon.MouseLeave += MenuButton_MouseLeave;

            this.Resize += (s, e) => ApplyGravity();
        }
        private void MenuButton_MouseLeave(object sender, EventArgs e)
        {
            if (!isFocused)
            {
                ResetStyle();
            }
        }

        private void MenuButton_MouseEnter(object sender, EventArgs e)
        {
            if (!isFocused)
            {
                this.BackColor = Color.FromArgb(240, 248, 255);
                textLabel.ForeColor = Color.Blue;
                icon.IconColor = Color.Blue;
                endIcon.IconColor = Color.Blue;
            }
        }

        private void ApplyFocusStyle()
        {
            if (isFocused)
            {
                this.BackColor = Color.FromArgb(240, 248, 255);
                textLabel.ForeColor = Color.Blue;
                icon.IconColor = Color.Blue;
                endIcon.IconColor = Color.Blue;
                textLabel.Font = new Font("Arial", 11, FontStyle.Bold);
            }
            else
            {
                ResetStyle();
            }
        }

        private void ResetStyle()
        {
            this.BackColor = Color.Transparent;
            textLabel.BackColor = Color.Transparent;
            icon.BackColor = Color.Transparent;
            endIcon.BackColor = Color.Transparent;

            textLabel.ForeColor = Color.Black;
            icon.IconColor = Color.Black;
            endIcon.IconColor = Color.Black;
        }

        private void ApplyGravity()
        {
            
            if (endIcon.IconChar == IconChar.None)
            {
                endIcon.Visible = false;  // Hide if no icon
            }
            else
            {
                endIcon.Visible = true;
            }


        }

        // Public Properties
        public string Title
        {
            get => textLabel.Text;
            set => textLabel.Text = value;
        }

        public IconChar Icon
        {
            get => icon.IconChar;
            set => icon.IconChar = value;
        }

        public IconChar EndIcon
        {
            get => endIcon.IconChar;
            set
            {
                endIcon.IconChar = value;
                ApplyGravity();
            }
        }

        public bool IsFocused
        {
            get => isFocused;
            set
            {
                isFocused = value;
                ApplyFocusStyle();
            }
        }

        public int IconSize
        {
            get => icon.IconSize;
            set
            {
                icon.IconSize = value;
                icon.Size = new Size(value, value);
                ApplyGravity();
            }
        }

        public Color IconColor
        {
            get => icon.IconColor;
            set => icon.IconColor = value;
        }

        public Color EndIconColor
        {
            get => endIcon.IconColor;
            set => endIcon.IconColor = value;
        }

        public Font Font
        {
            get => textLabel.Font;
            set => textLabel.Font = value;
        }

        public Color TextColor
        {
            get => textLabel.ForeColor;
            set => textLabel.ForeColor = value;
        }

        public MenuButtonGravity Gravity
        {
            get => gravity;
            set
            {
                gravity = value;
                ApplyGravity();
            }
        }

        public int IconMarginStart
        {
            get => iconMarginStart;
            set
            {
                iconMarginStart = value;
                ApplyGravity();
            }
        }
    }

}
