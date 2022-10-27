using BadBroker.API.Controllers;
using BadBroker.API.ViewModels;
using BadBroker.Application.Commands.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace BadBroker.Test.API
{
    public class RatesControllerTest
    {
        [Test]
        public async Task Execute_WhenAnyFilterValueIsEmpty_Returns400() {
            //Arrange
            Mock<IGetTheBestRateCommand> getTheBestRateCommandMock = new Mock<IGetTheBestRateCommand>();
            RatesController controller = new RatesController(getTheBestRateCommandMock.Object);

            var emptyFilter = new FilterViewModel(string.Empty, string.Empty, string.Empty);

            //Act
            controller.ModelState.AddModelError("Empty filter", "Some filter's values are empty");
            var response = await controller.GetBest(emptyFilter);

            //Assert
            var actual = response as BadRequestResult;
            Assert.NotNull(actual);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, actual.StatusCode);
        }
    }
}