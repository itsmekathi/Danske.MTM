using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Application.Services.Interfaces;
using Danske.MTM.Persistence.Models.MTM;
using System;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Services
{
    public class MunicipalityTaxScheduleService : IMunicipalityTaxScheduleService
    {
       private IMunicipalityTaxScheduleRepository _municipalityTaxScheduleRepository { get; }
        public MunicipalityTaxScheduleService(IMunicipalityTaxScheduleRepository municipalityTaxScheduleRepository)
        {
            _municipalityTaxScheduleRepository = municipalityTaxScheduleRepository;
        }
        public async Task<MunicipalityTaxSchedules> SearchMunicipalityTax(string municipalityName, DateTime date)
        {
            MunicipalityTaxSchedules taxSchedule = await _municipalityTaxScheduleRepository.GetTaxScheduleByDay(municipalityName, date);
            if(taxSchedule == null)
            {
               taxSchedule = await _municipalityTaxScheduleRepository.GetTaxScheduleByWeek(municipalityName, date);
            }
            if(taxSchedule == null)
            {
                taxSchedule = await _municipalityTaxScheduleRepository.GetTaxScheduleByMonth(municipalityName, date);
            }
            if(taxSchedule == null)
            {
                taxSchedule = await _municipalityTaxScheduleRepository.GetTaxScheduleByYear(municipalityName, date);
            }
            return taxSchedule;
        }
    }
}
