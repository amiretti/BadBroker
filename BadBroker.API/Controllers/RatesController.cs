using BadBroker.Application.Commands.Interfaces;
using BadBroker.Application.Dtos;
using BadBroker.Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BadBroker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IGetTheBestRateCommand _getTheBestRateCommand;

        public RatesController(IConfiguration config, IGetTheBestRateCommand getTheBestRateCommand)
        {
            _config = config;
            _getTheBestRateCommand = getTheBestRateCommand;
        }
        /// <summary>
        /// It determine what would be the best revenue for a specified historical period.It can be used for statistic purposes.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="moneyUsd"></param>
        /// <returns></returns>
        [HttpGet("best")]
        public async Task<ActionResult> GetBest([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] string moneyUsd)
        {
            if (string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate) || string.IsNullOrEmpty(moneyUsd))
            {
                return BadRequest("Yo should specify start date, end date and amount (in U$D)");
            }

            await _getTheBestRateCommand.Configure(new Models.Filter() { EndDate = endDate, StartDate = startDate, Amount = moneyUsd })
                                        .Execute();

            if (_getTheBestRateCommand.Success)
            {
                return Ok(_getTheBestRateCommand.Response);
            }
            else
            {
                return StatusCode(500, _getTheBestRateCommand.Message);
            }
        }
    }
}
