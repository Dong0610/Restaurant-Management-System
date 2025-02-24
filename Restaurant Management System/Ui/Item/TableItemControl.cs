using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Documents;
using Restaurant_Management_System.Entity;

namespace Restaurant_Management_System.Ui.Item
{
    public partial class TableItemControl: UserControl
    {
        private Tables table;
        private  Action<Tables> OnClickItem;
        public TableItemControl(Tables table,Action<Tables> action)
        {
            InitializeComponent();
            this.table = table;
            this.OnClickItem = action;

            txtTbName.Text = table.TableNumber;
            tbCapacity.Text = table.Capacity.ToString();


            if (table.Status == "Occupied")
            {
                this.BackColor = Color.FromArgb(255, 133, 102);
            }
            else if(table.Status== "Available")
            {
                this.BackColor = Color.FromArgb(102, 255, 102);
            }
           
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ItemClick(object sender, EventArgs e)
        {
            OnClickItem?.Invoke(table);
        }
    }
}
