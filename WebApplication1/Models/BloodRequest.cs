using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("BloodRequests")]
    public class BloodRequest
    {
        [Key]
        [Column("RequestId")]
        public int RequestId { get; set; }

        [Column("PatientName")]
        [Display(Name = "Full Name")]
        public string? Name { get; set; }   // made nullable

        [NotMapped]
        public string? PatientName
        {
            get => Name;
            set => Name = value;
        }

        [Column("Email")]
        [EmailAddress]
        public string? Email { get; set; }  // made nullable

        [Column("BloodType")]
        [Display(Name = "Blood Type")]
        public string? BloodType { get; set; }  // made nullable

        [Column("UnitsNeeded")]
        [Display(Name = "Units Needed")]
        [Range(1, 20, ErrorMessage = "You can request between 1 and 20 units.")]
        public int? Units { get; set; }  // nullable int, in case database has null

        [Column("RequestDate")]
        [DataType(DataType.Date)]
        [Display(Name = "Required Date")]
        public DateTime? RequiredDate { get; set; }  // nullable date

        [Column("Notes")]
        public string? Notes { get; set; }

        [Column("Status")]
        [Display(Name = "Request Status")]
        public string? Status { get; set; } = "Pending";

        [Column("Request")]
        [Display(Name = "Request Type")]
        public string? Request { get; set; }  // made nullable

        [Column("Hall")]
        public string? Hall { get; set; }

        [Column("Department")]
        public string? Department { get; set; }

        [Column("CreatedAt")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
    }
}
