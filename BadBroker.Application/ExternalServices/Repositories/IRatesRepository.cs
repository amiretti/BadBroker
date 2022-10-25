using BadBroker.Models;
using System.Threading.Tasks;

namespace BadBroker.Application.ExternalServices.Repositories
{
    public interface IRatesRepository
    {
        Task<TimeSeriesResponse> GetTimeSeries(Filter filter);
    }
}
