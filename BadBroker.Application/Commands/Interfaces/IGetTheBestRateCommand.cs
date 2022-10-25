using BadBroker.Application.Dtos;
using BadBroker.Models;

namespace BadBroker.Application.Commands.Interfaces
{
    public interface IGetTheBestRateCommand : ICommand
    {
        ICommand Configure(Filter filter);
        BestRateDto Response { get; }
    }
}
