using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("EMAILNOTIFICATION")]
    public class EmailNotification
    {
        [Key]
        [Column("NOTIFICATIONID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotificationId { get; set; }

        [Column("SUBJECT")]
        [StringLength(100)]
        [Display(Name = "Subject")]
        public string? Subject { get; set; }

        [Column("MESSAGE")]
        [Display(Name = "Message")]
        public string? Message { get; set; }

        [Required]
        [Column("DATESENT")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date Sent")]
        public DateTime DateSent { get; set; }

        [Required]
        [Column("RECEIPIENTID")]
        [Display(Name = "Recipient ID")]
        public int RecipientId { get; set; }

        // Navigation properties
        [ForeignKey("RecipientId")]
        public virtual Recipient Recipient { get; set; }
    }
}
