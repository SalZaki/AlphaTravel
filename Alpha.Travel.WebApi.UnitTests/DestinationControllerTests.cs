namespace Alpha.Travel.WebApi.UnitTests
{
    using NUnit.Framework;
    using System.Collections.Generic;
    using Application.Categories.Models;
    using WebApi.Controllers.V1;
    using MediatR;
    using Moq;
    using Alpha.Travel.Application.Destinations.Queries;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [TestFixture]
    public class DestinationControllerTests
    {
        private IEnumerable<DestinationPreviewDto> _destinations;

        [SetUp]
        public void Setup()
        {
            _destinations =
            new List<DestinationPreviewDto>
            {
                new DestinationPreviewDto
                {
                        Id = 1,
                        Description = "This is a test destination",
                        Name ="Test"
                }
            };
        }

        [Test]
        public async Task VerifyCanGetTravelProfile()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<DestinationPreviewDto>
            {
                new DestinationPreviewDto
                {
                        Id = 1,
                        Description = "This is a test destination",
                        Name ="Test"
                }
            });

            var sut = new DestinationsController(mockMediator.Object);

            // Act
            IActionResult result = await sut.GetDestinationByIdAsync(new GetDestinationPreviewQuery() { DestinationId = 1 }, CancellationToken.None);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }
    }
}