using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Tables
    {
        public Tables()
        {
            Orders = new HashSet<Orders>();
            Reservations = new HashSet<Reservations>();
        }

        public int TableId { get; set; }
        public string TableNumber { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
        public int? ReservationId { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
