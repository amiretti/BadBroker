using BadBroker.Application.Commands;
using BadBroker.Application.Commands.Interfaces;
using BadBroker.Application.ExternalServices;
using BadBroker.Application.ExternalServices.Repositories;
using BadBroker.Storage.ExternalAPIOperations;
using BadBroker.Storage.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BadBroker.Infrastructure
{
    public class IoCRegistry
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IGetTheBestRateCommand, GetTheBestRateCommand>();
            services.AddScoped<IRatesRepository, RatesRepository>();
            services.AddScoped<IExchangeRatesDataApi, ExchangeRatesDataApi>();
        }
    }
}
