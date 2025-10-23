using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("BLOODREQUEST")]
    public class BloodRequest
    {
        [Key]
        [Column("REQUESTID")]
        public int RequestId { get; set; }

        [Required]
        [Column("BLOODTYPEREQUIRED")]
        [StringLength(5)]
        [Display(Name = "Blood Type Required")]
        public string BloodTypeRequired { get; set; }

        [Column("QUANTITYREQUESTED")]
        [Display(Name = "Quantity Requested")]
        public int? QuantityRequested { get; set; }

        [Required]
        [Column("REQUESTEDDATE")]
        [DataType(DataType.Date)]
        [Display(Name = "Requested Date")]
        public DateOnly RequestedDate { get; set; }

        [Required]
        [Column("STATUS")]
        [StringLength(50)]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Pending";

        [Required]
        [Column("RECEIPIENTID")]
        [Display(Name = "Recipient ID")]
        public int RecipientId { get; set; }

        // Navigation properties
        [ForeignKey("RecipientId")]
        public virtual Recipient Recipient { get; set; }
    }
}
