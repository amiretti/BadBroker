namespace BadBroker.Infrastructure.Configuration.Interfaces
{
    public interface IAPIConfiguration
    {
        string ServiceUrl { get; set; }
        string ServiceKey { get; set; }
    }
}
