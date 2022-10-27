using BadBroker.Application.ExternalServices;
using BadBroker.Application.ExternalServices.Repositories;
using BadBroker.Models;
using System.Threading.Tasks;

namespace BadBroker.Storage.Repositories
{
    public class RatesRepository : IRatesRepository
    {
        private readonly IExchangeRatesDataApi _exchangeRatesData; 
        public RatesRepository(IExchangeRatesDataApi exchangeRatesDataApi)
        {
            _exchangeRatesData = exchangeRatesDataApi;
        }
        public async Task<TimeSeriesResponse> GetTimeSeries(Filter filter)
        {
            try
            {
                return await _exchangeRatesData.GetTimeSeries(new ExternalFilter(filter.StartDate, filter.EndDate));
            }
            catch
            {
                throw;
            }
        }
    }
}