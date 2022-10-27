using BadBroker.Application.Helpers;
using BadBroker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BadBroker.Application.Validations
{
    public class FilterValidations : IValidations<Filter>
    {
        public async Task<ICollection<string>> Validate(Filter entity)
        {
            var response = new List<string>();
            if (!DateTimeHelper.IsValidDate(entity.StartDate)) 
            {
                response.Add("Start date must be a date in yyyy-MM-dd format");
            }

            if (!DateTimeHelper.IsValidDate(entity.EndDate))
            {
                response.Add("End date must be a date in yyyy-MM-dd format");
            }

            if ((DateTimeHelper.ToDate(entity.EndDate) - DateTimeHelper.ToDate(entity.StartDate)).TotalDays > 60)
            {
                response.Add("The specified historical period cannot exceed 2 months (60 days)");
            }

            if (!DateTimeHelper.FirstIsSmallerThanSecond(entity.StartDate, entity.EndDate))
            {
                response.Add("Start date must be less than end date");
            }

            if (double.TryParse(entity.Amount, out double money))
            {
                if (money <= 0)
                    response.Add("The amount must be greater than zero.");
            }
            else
            {
                response.Add("The amount must be a numeric value.");
            }

            return response;
        }

        
    }
}