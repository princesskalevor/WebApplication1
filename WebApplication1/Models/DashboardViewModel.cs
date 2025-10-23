using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
   

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
