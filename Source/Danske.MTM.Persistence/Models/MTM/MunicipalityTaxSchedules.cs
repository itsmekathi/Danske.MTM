using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Danske.MTM.Persistence.Models.MTM
{
    public partial class MunicipalityTaxSchedules
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("TaxSheduleTypeID")]
        public int TaxSheduleTypeId { get; set; }
        [Required]
        [StringLength(200)]
        public string MunicipalityName { get; set; }
        [Column(TypeName = "date")]
        public DateTime FromDate { get; set; }
        [Column("TODate", TypeName = "date")]
        public DateTime? Todate { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        [ForeignKey(nameof(TaxSheduleTypeId))]
        [InverseProperty(nameof(TaxScheduleTypes.MunicipalityTaxSchedules))]
        public virtual TaxScheduleTypes TaxSheduleType { get; set; }
    }
}
