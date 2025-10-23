using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("RECEIPIENT")]
    public class Recipient
    {
        [Key]
        [Column("RECEIPIENTID")]
        public int RecipientId { get; set; }

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
        [Column("EMAIL")]
        [StringLength(100)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Column("PHONENUMBER")]
        [Display(Name = "Phone Number")]
        public int? PhoneNumber { get; set; }

        [Required]
        [Column("BLOODTYPE")]
        [StringLength(5)]
        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }

        [Column("QUANTITYREQUESTED")]
        [Display(Name = "Quantity Requested")]
        public int? QuantityRequested { get; set; }

        // Navigation properties
        public virtual ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();
        public virtual ICollection<EmailNotification> EmailNotifications { get; set; } = new List<EmailNotification>();
    }
}
