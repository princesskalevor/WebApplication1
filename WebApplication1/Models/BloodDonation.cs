using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("BLOODDONATION")]
    public class BloodDonation
    {
        [Key]
        [Column("DONATIONID")]
        public int DonationId { get; set; }

        [Required]
        [Column("BLOODTYPE")]
        [StringLength(5)]
        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }

        [Required]
        [Column("QUANTITY")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Column("DONATIONDATE")]
        [DataType(DataType.Date)]
        [Display(Name = "Donation Date")]
        public DateOnly? DonationDate { get; set; }

        [Required]
        [Column("DONORID")]
        [Display(Name = "Donor ID")]
        public int DonorId { get; set; }

        // Navigation properties
        [ForeignKey("DonorId")]
        public virtual Donor Donor { get; set; }
    }
}
