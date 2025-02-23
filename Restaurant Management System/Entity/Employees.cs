using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class Employees
    {
        public Employees()
        {
            Accounts = new HashSet<Accounts>();
            EmployeeAttendance = new HashSet<EmployeeAttendance>();
            Notifications = new HashSet<Notifications>();
            Orders = new HashSet<Orders>();
        }

        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public int PositionId { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Shift { get; set; }
        public string Status { get; set; }

        public virtual Positions Position { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        public virtual ICollection<EmployeeAttendance> EmployeeAttendance { get; set; }
        public virtual ICollection<Notifications> Notifications { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
