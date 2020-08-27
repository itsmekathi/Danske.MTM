using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Common.Enums;
using Danske.MTM.Persistence.Context;
using Danske.MTM.Persistence.Models.MTM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Repositories
{
    public class MunicipalityTaxScheduleRepository : IMunicipalityTaxScheduleRepository
    {
        private MTMContext _mtmContext;
        public MunicipalityTaxScheduleRepository(MTMContext mtmContext)
        {
            _mtmContext = mtmContext;
        }
        public async Task<IEnumerable<MunicipalityTaxSchedules>> GetAll()
        {
            return await (from tax in _mtmContext.MunicipalityTaxSchedules select tax).ToListAsync();
        }

        public async Task<MunicipalityTaxSchedules> GetById(int id)
        {
            return await _mtmContext.MunicipalityTaxSchedules.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task<MunicipalityTaxSchedules> SearchTaxSchedule(string municipalityName, DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<MunicipalityTaxSchedules> AddTaxSchedule(MunicipalityTaxSchedules municipalityTaxSchedule)
        {
            _mtmContext.MunicipalityTaxSchedules.Add(municipalityTaxSchedule);
            await _mtmContext.SaveChangesAsync();
            return await _mtmContext.MunicipalityTaxSchedules.FindAsync(municipalityTaxSchedule.Id);
        }

        public async Task<MunicipalityTaxSchedules> UpdateTaxSchedule(MunicipalityTaxSchedules municipalityTaxSchedule)
        {
            return await AddTaxSchedule(municipalityTaxSchedule);
        }

        public async Task<int> BulkInsertTaxSchedule(IEnumerable<MunicipalityTaxSchedules> municipalityTaxSchedules)
        {
            _mtmContext.MunicipalityTaxSchedules.AddRange(municipalityTaxSchedules);
            int updateCount = await _mtmContext.SaveChangesAsync();
            return updateCount;
        }

        public async Task<MunicipalityTaxSchedules> GetByDay(string municipalityName, DateTime date)
        {
            return await _mtmContext.MunicipalityTaxSchedules
               .FirstOrDefaultAsync(_ => _.TaxSheduleTypeId == (int)eTaxScheduleTypes.Daily
               && _.MunicipalityName == municipalityName
               && _.FromDate.Date == date.Date);
        }

        public async Task<MunicipalityTaxSchedules> GetByWeek(string municipalityName, DateTime date)
        {
            return await GetByType(municipalityName, date, eTaxScheduleTypes.Weekly);
        }

        public async Task<MunicipalityTaxSchedules> GetByMonth(string municipalityName, DateTime date)
        {
            return await GetByType(municipalityName, date, eTaxScheduleTypes.Monthly);
        }


        public async Task<MunicipalityTaxSchedules> GetByYear(string municipalityName, DateTime date)
        {
            return await GetByType(municipalityName, date, eTaxScheduleTypes.Yearly);
        }

        private async Task<MunicipalityTaxSchedules> GetByType(string municipalityName, DateTime date, eTaxScheduleTypes type)
        {
            return await _mtmContext.MunicipalityTaxSchedules
               .FirstOrDefaultAsync(_ => _.TaxSheduleTypeId == (int)type
               && _.MunicipalityName == municipalityName
               && date.Date >= _.FromDate.Date && date.Date <= _.Todate.Value.Date);
        }
    }
}
