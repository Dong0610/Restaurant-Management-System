using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class CustomerFeedback
    {
        public int FeedbackId { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderId { get; set; }
        public int? Rating { get; set; }
        public string Comments { get; set; }
        public DateTime DateSubmitted { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual Orders Order { get; set; }
    }
}
