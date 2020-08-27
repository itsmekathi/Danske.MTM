using Danske.MTM.Persistence.Models.MTM;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Repositories.Interfaces
{
    public interface IMunicipalityTaxScheduleRepository
    {
        Task<IEnumerable<MunicipalityTaxSchedules>> GetAll();
        Task<MunicipalityTaxSchedules> GetById(int id);

        Task<MunicipalityTaxSchedules> SearchTaxSchedule(string municipalityName, DateTime date);
        Task<MunicipalityTaxSchedules> AddTaxSchedule(MunicipalityTaxSchedules municipalityTaxSchedule);
        Task<MunicipalityTaxSchedules> UpdateTaxSchedule(MunicipalityTaxSchedules municipalityTaxSchedule);
        Task<int> BulkInsertTaxSchedule(IEnumerable<MunicipalityTaxSchedules> municipalityTaxSchedules);


        Task<MunicipalityTaxSchedules> GetByDay(string municipalityName, DateTime date);
        Task<MunicipalityTaxSchedules> GetByWeek(string municipalityName, DateTime date);
        Task<MunicipalityTaxSchedules> GetByMonth(string municipalityName, DateTime date);
        Task<MunicipalityTaxSchedules> GetByYear(string municipalityName, DateTime date);
    }
}
