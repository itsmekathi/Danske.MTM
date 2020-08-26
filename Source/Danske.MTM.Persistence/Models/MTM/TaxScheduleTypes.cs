using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Danske.MTM.Persistence.Models.MTM
{
    public partial class TaxScheduleTypes
    {
        public TaxScheduleTypes()
        {
            MunicipalityTaxSchedules = new HashSet<MunicipalityTaxSchedules>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("TaxSheduleType")]
        public virtual ICollection<MunicipalityTaxSchedules> MunicipalityTaxSchedules { get; set; }
    }
}
