using System.Collections.Generic;

namespace BadBroker.Models
{
    public class ExternalFilter
    {
        public ExternalFilter(string startDate, string endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            Base = Constants.CURRENCY_USD;
            Symbols = new List<string>() { Constants.CURRENCY_EUR, Constants.CURRENCY_GBP, Constants.CURRENCY_JPY, Constants.CURRENCY_RUB };
        }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Base { get; set; }
        public List<string> Symbols { get; set; }
    }
}