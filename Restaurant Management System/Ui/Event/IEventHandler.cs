using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Management_System.Ui.Event
{
   
    public interface IEventHandler
    {
        void OnFormSearch(string message);
        void OnReloadData();
    }
}
