using Danske.MTM.Persistence.Models.MTM;
using System;
using System.Threading.Tasks;

namespace Danske.MTM.Application.Services.Interfaces
{
    public interface IMunicipalityTaxScheduleService
    {
        Task<MunicipalityTaxSchedules> SearchMunicipalityTax(string municipalityName, DateTime date);
    }
}
