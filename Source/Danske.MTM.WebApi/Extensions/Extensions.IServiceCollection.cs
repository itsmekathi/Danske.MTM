using Danske.MTM.Application.Repositories;
using Danske.MTM.Application.Repositories.Interfaces;
using Danske.MTM.Application.Services;
using Danske.MTM.Application.Services.Interfaces;
using Danske.MTM.Common.Infrastructure;
using Danske.MTM.Common.Interfaces;
using Danske.MTM.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Danske.MTM.WebApi.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddAllApplicationServices(this IServiceCollection services)
        {
            services.AddApplicationDbContext();
            services.AddApplicationServices();

            return services;
        }
        private static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MTMContext>();
            return services;
        }
        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ILogger, ConsoleLogger>();
            services.AddSingleton<IMunicipalityTaxScheduleRepository, MunicipalityTaxScheduleRepository>();
            services.AddSingleton<IMunicipalityTaxScheduleService, MunicipalityTaxScheduleService>();
            return services;
        }
    }
}
