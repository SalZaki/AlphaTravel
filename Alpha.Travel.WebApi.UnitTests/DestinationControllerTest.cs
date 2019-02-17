namespace Alpha.Travel.WebApi.UnitTests
{
    using NUnit.Framework;
    using FluentAssertions;
    using WebApi.Controllers.V1;
    using MediatR;
    using Moq;
    using Application.Destinations.Models;
    using Application.Destinations.Queries;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [TestFixture]
    public class DestinationControllerTest
    {
        private DestinationPreviewDto _destination;
        private List<DestinationPreviewDto> _destinations;

        [SetUp]
        public void Setup()
        {
            _destination = new DestinationPreviewDto
            {
                Id = 1,
                Description = "This is a test destination",
                Name = "Test"
            };

            _destinations = new List<DestinationPreviewDto>()
            {
                new DestinationPreviewDto
                {
                    Id = 1,
                    Description = "This is a test destination",
                    Name = "Test1"
                },

                new DestinationPreviewDto
                {
                    Id = 2,
                    Description = "This is a test 2 destination",
                    Name = "Test2"
                },

                new DestinationPreviewDto
                {
                    Id = 3,
                    Description = "This is a test 3 destination",
                    Name = "Test3"
                }
            };
        }

        [Test]
        public async Task Destination_GetByIdAsync_GetDestinationPreviewQuery_With_Correct_Data()
        {
            // Arrange
            var query = new GetDestinationPreviewQuery { Id = 1 };
            var mediator = new Mock<IMediator>();
            var sut = new DestinationController(mediator.Object);

            // Act
            await sut.GetByIdAsync(query, default(CancellationToken));

            // Assert
            mediator.Verify(x => x.Send(It.Is<GetDestinationPreviewQuery>(y => y.Id == query.Id), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Destination_GetByIdAsync_Returns_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_destination);
            var sut = new DestinationController(mockMediator.Object);
            var query = new GetDestinationPreviewQuery { Id = 1 };

            // Act
            var result = await sut.GetByIdAsync(query, default(CancellationToken));

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Equals(200);
        }

        [Test]
        public async Task Destination_GetByIdAsync_Returns_Fail()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((DestinationPreviewDto)null).Verifiable();
            var sut = new DestinationController(mockMediator.Object);
            var query = new GetDestinationPreviewQuery { Id = 22 };

            // Act
            var result = await sut.GetByIdAsync(query, default(CancellationToken));

            // Assert
            var okResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            okResult.StatusCode.Should().Equals(404);
        }

        [Test]
        public async Task Destination_GetByIdAsync_Returns_Correct_Result()
        {
            // Arranges
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_destination);
            var sut = new DestinationController(mockMediator.Object);
            var query = new GetDestinationPreviewQuery { Id = 1 };

            // Act
            var result = await sut.GetByIdAsync(query, default(CancellationToken));

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var destination = okResult.Value.Should().BeAssignableTo<DestinationPreviewDto>().Subject;
            destination.Id.Should().Be(1);
            destination.Name.Should().Be("Test");
        }

        [Test]
        public async Task Destination_GetAllAsync_Returns_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(_destinations);
            var sut = new DestinationController(mockMediator.Object);
            var query = new GetDestinationsPreviewQuery();

            // Act
            var result = await sut.GetAllAsync(query, default(CancellationToken));

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Equals(200);
        }
    }
}