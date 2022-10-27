using BadBroker.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BadBroker.API.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel() { }

        public FilterViewModel(string startDate, string endDate, string amount)
        {
            StartDate = startDate.Trim();
            EndDate = endDate.Trim();
            MoneyUsd = amount.Trim();
        }

        [Required]
        [MinLength(10), MaxLength(10)]
        public string StartDate { get; set; }

        [Required]
        [MinLength(10), MaxLength(10)]
        public string EndDate { get; set; }

        [BindRequired]
        [MinLength(1)]
        public string MoneyUsd { get; set; }

        public Filter ToModel() {
            return new Filter()
            {
                Amount = MoneyUsd,
                EndDate = EndDate,
                StartDate = StartDate
            };
        }
    }
}
