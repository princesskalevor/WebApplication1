using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BloodRequest
    {
        [Key]
        public int RequestId { get; set; }  // Primary key

        public string PatientName { get; set; }
        public string BloodType { get; set; }
        public string Hall { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public int UnitsNeeded { get; set; }
    }

    public class DashboardViewModel
    {
        public int TotalDonors { get; set; }
        public int TotalDonations { get; set; }
        public int TotalRequests { get; set; }
        public int AvailableUnits { get; set; }

        // Uncomment this so you can show recent requests on the dashboard
        public List<BloodRequest> RecentRequests { get; set; } = new List<BloodRequest>();
    }
}
