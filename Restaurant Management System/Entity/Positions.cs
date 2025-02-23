using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Positions
    {
        public Positions()
        {
            Employees = new HashSet<Employees>();
        }

        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public decimal BaseSalary { get; set; }

        public virtual ICollection<Employees> Employees { get; set; }
    }
}
