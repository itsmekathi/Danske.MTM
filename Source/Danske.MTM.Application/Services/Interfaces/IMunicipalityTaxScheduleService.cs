using Danske.MTM.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Services.Interfaces
{
    public interface IMunicipalityTaxScheduleService
    {
        Task<IEnumerable<MunicipalityTaxScheduleDto>> GetAllMunicipalityTax();
        Task<MunicipalityTaxScheduleDto> GetMunicipalityTaxById(int id);
        Task<MunicipalityTaxScheduleDto> SearchMunicipalityTax(string municipalityName, DateTime date);
        Task<MunicipalityTaxScheduleDto> AddNewMunicipalityTax(MunicipalityTaxScheduleDto municipalityTaxScheduleDto);
        Task<MunicipalityTaxScheduleDto> UpdateExistingMunicipalityTax( int id, MunicipalityTaxScheduleDto municipalityTaxScheduleDto);
        Task<int> BulkAddMunicipalityTax(IEnumerable<MunicipalityTaxScheduleDto> municipalityTaxScheduleDtos);
    }
}
