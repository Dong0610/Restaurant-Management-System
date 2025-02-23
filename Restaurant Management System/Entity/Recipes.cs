using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Recipes
    {
        public int RecipeId { get; set; }
        public int? ItemId { get; set; }
        public int? IngredientId { get; set; }
        public int Quantity { get; set; }

        public virtual Ingredients Ingredient { get; set; }
        public virtual MenuItems Item { get; set; }
    }
}
