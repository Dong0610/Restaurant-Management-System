using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cafe_Management_System.Ui.Event;
using Microsoft.EntityFrameworkCore;
using Restaurant_Management_System.Entity;
using Restaurant_Management_System.Ui.Item;

namespace Cafe_Management_System.Ui.Control
{
    public partial class HomeControl : UserControl, IEventHandler
    {

        private Timer resizeTimer;
        private List<Tables> listTable = new List<Tables>();

        public HomeControl()
        {
            InitializeComponent();
            resizeTimer = new Timer();
            resizeTimer.Interval = 200; // Delay time in milliseconds
            listTable = Database.Instance.AppDatabase().Tables.ToList();

            LoadTable(listTable);
        }

        public void OnFormSearch(string message)
        {
            if (message.Length == 0)
            {
                LoadTable(listTable);
            }
            else
            {
                List<Tables> searchList = new List<Tables>();

                foreach (var item in listTable)
                {
                    if (item.TableNumber.ToLower().Contains(message.ToLower()) || item.Status.ToLower().Contains(message.ToLower()))
                    {
                        searchList.Add(item);
                    }
                }
                LoadTable(searchList);
            }
        }

        public void OnReloadData()
        {
            LoadTable(Database.Instance.AppDatabase().Tables.ToList());
        }

        void LoadTable(List<Tables> listTable)
        {
            flTableLayout.SuspendLayout(); 
            flTableLayout.Controls.Clear();
            var controls = listTable.Select(tb =>
            {
                var control = new TableItemControl(tb, (Tables rs) =>
                {
                 
                });
                control.Margin = new Padding(4);
                return control;
            }).ToList();
            flTableLayout.Controls.AddRange(controls.ToArray());
            flTableLayout.ResumeLayout(); // Resume layout updates

        }



    }
}
