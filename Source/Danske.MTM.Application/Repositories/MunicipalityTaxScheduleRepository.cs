using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Common.Enums;
using Danske.MTM.Persistence.Context;
using Danske.MTM.Persistence.Models.MTM;
using Microsoft.EntityFrameworkCore;
using System;
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
        public async Task<MunicipalityTaxSchedules> GetTaxScheduleByDay(string municipalityName, DateTime date)
        {
            return await _mtmContext.MunicipalityTaxSchedules
                .FirstOrDefaultAsync(_ => _.TaxSheduleTypeId == (int)eTaxScheduleTypes.Daily
                && _.MunicipalityName == municipalityName
                && _.FromDate.Date == date.Date);
        }

        public async Task<MunicipalityTaxSchedules> GetTaxScheduleByWeek(string municipalityName, DateTime date)
        {
            return await _mtmContext.MunicipalityTaxSchedules
               .FirstOrDefaultAsync(_ => _.TaxSheduleTypeId == (int)eTaxScheduleTypes.Weekly
               && _.MunicipalityName == municipalityName
               && _.FromDate.Date >= date.Date && _.Todate.Value.Date <= date.Date);
        }

        public async Task<MunicipalityTaxSchedules> GetTaxScheduleByMonth(string municipalityName, DateTime date)
        {
            return await _mtmContext.MunicipalityTaxSchedules
               .FirstOrDefaultAsync(_ => _.TaxSheduleTypeId == (int)eTaxScheduleTypes.Monthly
               && _.MunicipalityName == municipalityName
               && _.FromDate.Date >= date.Date && _.Todate.Value.Date <= date.Date);
        }


        public async Task<MunicipalityTaxSchedules> GetTaxScheduleByYear(string municipalityName, DateTime date)
        {
            return await _mtmContext.MunicipalityTaxSchedules
               .FirstOrDefaultAsync(_ => _.TaxSheduleTypeId == (int)eTaxScheduleTypes.Yearly
               && _.MunicipalityName == municipalityName
               && _.FromDate.Date >= date.Date && _.Todate.Value.Date <= date.Date);
        }
    }
}
