using Danske.MTM.Persistence.Models.MTM;
using System;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Repositories.Interfaces
{
    public interface IMunicipalityTaxScheduleRepository
    {
        Task<MunicipalityTaxSchedules> GetTaxScheduleByDay(string municipalityName, DateTime date);
        Task<MunicipalityTaxSchedules> GetTaxScheduleByWeek(string municipalityName, DateTime date);
        Task<MunicipalityTaxSchedules> GetTaxScheduleByMonth(string municipalityName, DateTime date);
        Task<MunicipalityTaxSchedules> GetTaxScheduleByYear(string municipalityName, DateTime date);
    }
}
