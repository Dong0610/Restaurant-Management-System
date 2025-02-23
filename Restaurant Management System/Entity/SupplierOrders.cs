using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class SupplierOrders
    {
        public SupplierOrders()
        {
            SupplierPayments = new HashSet<SupplierPayments>();
        }

        public int SupplierOrderId { get; set; }
        public int? SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalCost { get; set; }
        public string Status { get; set; }

        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<SupplierPayments> SupplierPayments { get; set; }
    }
}
