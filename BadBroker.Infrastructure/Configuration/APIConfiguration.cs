using BadBroker.Infrastructure.Configuration.Interfaces;

namespace BadBroker.Infrastructure.Configuration
{
    public class APIConfiguration : IAPIConfiguration
    {
        public string ServiceUrl { get; set; }
        public string ServiceKey { get; set; }
    }
}
