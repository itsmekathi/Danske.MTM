using Danske.MTM.Application.Dtos;
using Danske.MTM.Common.Enums;

namespace Danske.MTM.Application.Helpers
{
    public static class MunicipalityTaxScheduleValidator
    {
        public static bool IsValid(MunicipalityTaxScheduleDto municipalityTaxScheduleDto)
        {
            bool result = false;

            switch(municipalityTaxScheduleDto.TaxSheduleTypeId)
            {
                case (int)eTaxScheduleTypes.Daily:
                    result = municipalityTaxScheduleDto.Todate == null && decimal.Compare(municipalityTaxScheduleDto.TaxAmount, 0) == 1 ;
                    break;
                case (int)eTaxScheduleTypes.Monthly:
                case (int)eTaxScheduleTypes.Weekly:
                case (int)eTaxScheduleTypes.Yearly:
                    result = municipalityTaxScheduleDto.Todate != null && decimal.Compare(municipalityTaxScheduleDto.TaxAmount, 0) == 1 && municipalityTaxScheduleDto.FromDate < municipalityTaxScheduleDto.Todate;
                    break;
            }
            return result;
        }
    }
}
