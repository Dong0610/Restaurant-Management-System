using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Restaurant_Management_System.Entity
{
    public partial class EmployeeAttendance
    {
        public int AttendanceId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public string Shift { get; set; }
        public string Status { get; set; }

        public virtual Employees Employee { get; set; }
    }
}
