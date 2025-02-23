using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Ingredients = new HashSet<Ingredients>();
            SupplierOrders = new HashSet<SupplierOrders>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int DeliveryTime { get; set; }

        public virtual ICollection<Ingredients> Ingredients { get; set; }
        public virtual ICollection<SupplierOrders> SupplierOrders { get; set; }
    }
}
