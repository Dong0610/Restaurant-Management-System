﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int ItemId { get; set; }
        public int QuantityInStock { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual MenuItems Item { get; set; }
    }
}
