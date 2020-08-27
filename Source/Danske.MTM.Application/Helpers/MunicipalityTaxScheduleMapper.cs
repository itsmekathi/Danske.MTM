using Danske.MTM.Application.Dtos;
using Danske.MTM.Persistence.Models.MTM;

namespace Danske.MTM.Application.Helpers
{
    public static class MunicipalityTaxScheduleMapper
    {
        public static MunicipalityTaxScheduleDto ToDto(MunicipalityTaxSchedules municipalityTaxSchedule)
        {
            return new MunicipalityTaxScheduleDto()
            {
                Id = municipalityTaxSchedule.Id,
                MunicipalityName = municipalityTaxSchedule.MunicipalityName,
                TaxSheduleTypeId = municipalityTaxSchedule.TaxSheduleTypeId,
                TaxAmount = municipalityTaxSchedule.TaxAmount,
                FromDate = municipalityTaxSchedule.FromDate,
                Todate = municipalityTaxSchedule.Todate
            };
        }

        public static MunicipalityTaxSchedules ToEntity(MunicipalityTaxScheduleDto municipalityTaxScheduleDto)
        {
            return new MunicipalityTaxSchedules()
            {
                Id = municipalityTaxScheduleDto.Id,
                MunicipalityName = municipalityTaxScheduleDto.MunicipalityName,
                TaxSheduleTypeId = municipalityTaxScheduleDto.TaxSheduleTypeId,
                TaxAmount = municipalityTaxScheduleDto.TaxAmount,
                FromDate = municipalityTaxScheduleDto.FromDate,
                Todate = municipalityTaxScheduleDto.Todate
            };
        }
    }
}
