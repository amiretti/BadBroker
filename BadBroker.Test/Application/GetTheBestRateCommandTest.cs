using BadBroker.Application.Commands;
using BadBroker.Application.ExternalServices.Repositories;
using BadBroker.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BadBroker.Test.Application
{
    public class GetTheBestRateCommandTest
    {
        private Mock<IRatesRepository> _ratesRepositoryMock;
        private TimeSeriesResponse _response;

        [SetUp]
        public void Init()
        {
            _ratesRepositoryMock = new Mock<IRatesRepository>();
            _response = GetMockResponse();
        }

        [Test]
        [TestCase("2020-12-09","2020-11-15")]
        [TestCase("2020-03-23", "2020-03-22")]
        [TestCase("2000-01-30", "2000-01-01")]
        [TestCase("1900-02-09", "1900-01-01")]
        [TestCase("2022-05-09", "2022-05-08")]
        public async Task Execute_WhenTheStartDateIsGreaterThenEndDate_SuccessFalse(string startDate, string endDate)
        {
            //Arrange
            var filter = new Filter() { StartDate = startDate, EndDate = endDate, Amount = "100" };
            _ratesRepositoryMock.Setup(r => r.GetTimeSeries(filter)).Returns(Task.Run(() => _response));
            var command = new GetTheBestRateCommand(_ratesRepositoryMock.Object);

            //Act
            await command.Configure(filter).Execute();

            //Assert
            Assert.IsFalse(command.Success);
            Assert.IsFalse(string.IsNullOrEmpty(command.Message));
        }

        [Test]
        [TestCase("2020-08-09", "2020-11-15")]
        [TestCase("2020-01-10", "2020-03-22")]
        [TestCase("2000-01-30", "2000-05-01")]
        [TestCase("1900-02-09", "1900-10-01")]
        [TestCase("2008-05-09", "2022-05-08")]
        public async Task Execute_WhenTheDiferenceBetweenStartDateAndEndDateIsGreaterThan60_SuccessFalse(string startDate, string endDate)
        {
            //Arrange
            var filter = new Filter() { StartDate = startDate, EndDate = endDate, Amount = "100" };
            _ratesRepositoryMock.Setup(r => r.GetTimeSeries(filter)).Returns(Task.Run(() => _response));
            var command = new GetTheBestRateCommand(_ratesRepositoryMock.Object);

            //Act
            await command.Configure(filter).Execute();

            //Assert
            Assert.IsFalse(command.Success);
            Assert.IsFalse(string.IsNullOrEmpty(command.Message));
        }


        [Test]
        [TestCase("-100")]
        [TestCase("-1")]
        [TestCase("-9999")]
        [TestCase("-1000000000")]
        [TestCase("-1.6")]
        [TestCase("0")]
        [TestCase("-0.01")]
        public async Task Execute_WhenTheMoneyUSDIsLowerThanZero_SuccessFalse(string moneyUsd)
        {
            //Arrange
            var filter = new Filter() { StartDate = "2020-08-09", EndDate = "2020-08-25", Amount = moneyUsd };
            _ratesRepositoryMock.Setup(r => r.GetTimeSeries(filter)).Returns(Task.Run(() => _response));
            var command = new GetTheBestRateCommand(_ratesRepositoryMock.Object);

            //Act
            await command.Configure(filter).Execute();

            //Assert
            Assert.IsFalse(command.Success);
            Assert.IsFalse(string.IsNullOrEmpty(command.Message));
        }


        [Test]
        [TestCase("2020-08-09", "2020-08-15", "100")]
        [TestCase("2020-08-09", "2020-08-15", "100000000000")]
        [TestCase("2014-08-01", "2014-09-30", "100")]
        [TestCase("2014-12-15", "2014-12-20", "0.001")]
        [TestCase("2014-12-15", "2014-12-20", "0.1")]
        [TestCase("2014-12-15", "2014-12-20", "999999")]
        [TestCase("2014-12-15", "2014-12-15", "3")]
        public async Task Execute_WhenTheFiltersAreOk_Success(string startDate, string endDate, string moneyUsd)
        {
            //Arrange
            var filter = new Filter() { StartDate = startDate, EndDate = endDate, Amount = moneyUsd };
            _ratesRepositoryMock.Setup(r => r.GetTimeSeries(filter)).Returns(Task.Run(() => _response));
            var command = new GetTheBestRateCommand(_ratesRepositoryMock.Object);

            //Act
            await command.Configure(filter).Execute();

            //Assert
            Assert.IsTrue(command.Response.Rates.Count > 0);
            Assert.IsTrue(command.Success);
            Assert.IsTrue(string.IsNullOrEmpty(command.Message));
        }

        #region Private Methods
        private TimeSeriesResponse GetMockResponse()
        {
            return new TimeSeriesResponse()
            {
                Base = Constants.CURRENCY_USD,
                EndDate = "2014-12-20",
                StartDate = "2014-12-15",
                Rates = GetMockRates(),
                Success = true,
                Timeseries = true
            };
        }
        private Dictionary<string, Rate> GetMockRates()
        {
            Dictionary<string, Rate> response = new Dictionary<string, Rate>
            {
                { "2014-12-15", new Rate() { EUR = 0.803818, GBP = 0.63935, JPY = 118.084399, RUB = 57.9957 } },
                { "2014-12-16", new Rate() { EUR = 0.799252, GBP = 0.634963, JPY = 116.831, RUB = 68.53245 } },
                { "2014-12-17", new Rate() { EUR = 0.810611, GBP = 0.642142, JPY = 118.468099, RUB = 68.30813 } },
                { "2014-12-18", new Rate() { EUR = 0.813927, GBP = 0.638237, JPY = 118.569499, RUB = 61.891725 } },
                { "2014-12-19", new Rate() { EUR = 0.817716, GBP = 0.639964, JPY = 119.3527, RUB = 58.9055 } },
                { "2014-12-20", new Rate() { EUR = 0.817703, GBP = 0.63999, JPY = 119.4555, RUB = 58.9055 } }
            };

            return response;
        }
        #endregion
    }
}