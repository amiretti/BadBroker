using BadBroker.Models;
using System.Threading.Tasks;

namespace BadBroker.Application.ExternalServices
{
    public interface IExchangeRatesDataApi
    {
        Task<TimeSeriesResponse> GetTimeSeries(ExternalFilter filter); 
    }
}