using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("BLOODINVENTORY")]
    public class BloodInventory
    {
        [Key]
        [Column("UNITID")]
        public int UnitId { get; set; }

        [Required]
        [Column("BLOODTYPE")]
        [StringLength(5)]
        [Display(Name = "Blood Type")]
        public string BloodType { get; set; }

        [Column("QUANTITY")]
        [Display(Name = "Quantity")]
        public int? Quantity { get; set; }

        [Required]
        [Column("EXPIRATIONDATE")]
        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Column("STORAGELOCATION")]
        [StringLength(100)]
        [Display(Name = "Storage Location")]
        public string? StorageLocation { get; set; }

        [Column("STATUS")]
        [StringLength(50)]
        [Display(Name = "Status")]
        public string? Status { get; set; }
    }
}
