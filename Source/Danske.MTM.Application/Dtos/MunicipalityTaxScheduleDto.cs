using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Danske.MTM.Application.Dtos
{
    public class MunicipalityTaxScheduleDto
    {
        [Required]
        public int TaxSheduleTypeId { get; set; }
        [Required]
        [StringLength(200)]
        public string MunicipalityName { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        
        public DateTime? Todate { get; set; }
    }
}
