using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("DONOR")]
    public class Donor
    {
        [Key]
        [Column("DONORID")]
        public int DonorId { get; set; }

        [Required]
        [Column("FIRSTNAME")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Column("LASTNAME")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Column("PHONENUMBER")]
        [StringLength(15)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Column("EMAILADDRESS")]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string? EmailAddress { get; set; }

        [Required]
        [Column("BLOODTYPE")]
        [StringLength(5)]
        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }

        [Required]
        [Column("MEDICALHISTORY")]
        [Display(Name = "Medical History")]
        public string MedicalHistory { get; set; }

        [Column("QRCODE")]
        [StringLength(300)]
        [Display(Name = "QR Code")]
        public string? QrCode { get; set; }

        // Navigation properties
        public virtual ICollection<BloodDonation> BloodDonations { get; set; } = new List<BloodDonation>();
    }
}
