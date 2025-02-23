using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Ingredients
    {
        public Ingredients()
        {
            Recipes = new HashSet<Recipes>();
        }

        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public int? SupplierId { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; }

        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<Recipes> Recipes { get; set; }
    }
}
