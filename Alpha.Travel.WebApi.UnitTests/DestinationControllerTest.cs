namespace Alpha.Travel.WebApi.UnitTests
{
    using NUnit.Framework;
    using FluentAssertions;
    using WebApi.Controllers.V1;
    using MediatR;
    using Moq;
    using Application.Categories.Models;
    using Application.Destinations.Queries;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    [TestFixture]
    public class DestinationControllerTest
    {
        //private DestinationPreviewDto _destination;

        //[SetUp]
        //public void Setup()
        //{
        //    _destination = new DestinationPreviewDto
        //    {
        //        Id = 1,
        //        Description = "This is a test destination",
        //        Name = "Test"
        //    };
        //}

        [Test]
        public async Task Destination_GetByIdAsync_GetDestinationPreviewQuery_With_Correct_Data()
        {
            var query = new GetDestinationPreviewQuery { Id = 1 };
            var mediator = new Mock<IMediator>();

            var sut = new DestinationController(mediator.Object);
            await sut.GetByIdAsync(query, default(CancellationToken));

            mediator.Verify(x => x.Send(It.Is<GetDestinationPreviewQuery>(y => y.Id == query.Id), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Destination_GetByIdAsync_Returns_Success()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DestinationPreviewDto
                {
                    Id = 1,
                    Description = "This is a test destination",
                    Name = "Test"
                });

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
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DestinationPreviewDto
                {
                    Id = 1,
                    Description = "This is a test destination",
                    Name = "Test"
                });

            var sut = new DestinationController(mockMediator.Object);
            var query = new GetDestinationPreviewQuery { Id = 1 };

            // Act
            var result = await sut.GetByIdAsync(query, default(CancellationToken));

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var destination = okResult.Value.Should().BeAssignableTo<DestinationPreviewDto>().Subject;
            destination.Id.Should().Be(1);
        }
    }
}