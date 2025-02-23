using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Reservations
    {
        public int ReservationId { get; set; }
        public int? CustomerId { get; set; }
        public int? TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Tables Table { get; set; }
    }
}
