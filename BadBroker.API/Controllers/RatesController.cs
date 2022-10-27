using BadBroker.API.ViewModels;
using BadBroker.Application.Commands.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BadBroker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IGetTheBestRateCommand _getTheBestRateCommand;

        public RatesController(IGetTheBestRateCommand getTheBestRateCommand)
        {
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
        public async Task<ActionResult> GetBest([FromQuery] FilterViewModel filter)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await _getTheBestRateCommand.Configure(filter.ToModel()).Execute();

            if (_getTheBestRateCommand.Success)
                return Ok(_getTheBestRateCommand.Response);
            else
                return StatusCode(500, _getTheBestRateCommand.Message);
        }
    }
}