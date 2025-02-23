using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Orders
    {
        public Orders()
        {
            CustomerFeedback = new HashSet<CustomerFeedback>();
            Invoices = new HashSet<Invoices>();
            OrderDetails = new HashSet<OrderDetails>();
        }

        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public int? TableId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderStatus { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Employees Employee { get; set; }
        public virtual Tables Table { get; set; }
        public virtual ICollection<CustomerFeedback> CustomerFeedback { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
