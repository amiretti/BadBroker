using System.Threading.Tasks;

namespace BadBroker.Application.Commands.Interfaces
{
    public interface ICommand
    {
        string Message { get; set; }
        bool Success { get; set; }
        Task Execute();
    }
}