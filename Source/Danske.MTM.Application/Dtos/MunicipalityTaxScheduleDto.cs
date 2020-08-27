using System;
using System.ComponentModel.DataAnnotations;

namespace Danske.MTM.Application.Dtos
{
    public class MunicipalityTaxScheduleDto
    {
        public int Id { get; set; }
        [Required]
        public int TaxSheduleTypeId { get; set; }
        [Required]
        [StringLength(200)]
        public string MunicipalityName { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        
        public DateTime? Todate { get; set; }
    }
}
