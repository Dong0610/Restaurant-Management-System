using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Customers
    {
        public Customers()
        {
            CustomerFeedback = new HashSet<CustomerFeedback>();
            Orders = new HashSet<Orders>();
            Reservations = new HashSet<Reservations>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }
        public int LoyaltyPoints { get; set; }

        public virtual ICollection<CustomerFeedback> CustomerFeedback { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
