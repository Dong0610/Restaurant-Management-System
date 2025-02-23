using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_Cafe_Manager.Witget
{
    public delegate void ActionControl();

    public class ControlItem
    {
        public string Text { get; set; }
        public Action OnClick { get; set; } // Optional: Add an action for click events
    }
}
