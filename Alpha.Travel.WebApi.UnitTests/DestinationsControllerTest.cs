namespace Alpha.Travel.WebApi.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Destinations.Models;
    using Application.Destinations.Queries;
    using NUnit.Framework;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Controllers.V1;
    using Application.Common.Models.Requests;

    [TestFixture]
    public class DestinationsControllerTest : BaseTest
    {
        [Test]
        public void Destinations_GetAllAsync_Has_HttpGet_Attribute()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var sut = new DestinationsController(mediator.Object);

            // Act
            var attribute = sut.GetAttributesOn(x => x.GetAllAsync(It.IsAny<PagingOptions>(), It.IsAny<CancellationToken>())).OfType<HttpGetAttribute>().SingleOrDefault();

            // Assert
            attribute.Should().NotBeNull();
            attribute.Name.Should().Be("GetAllAsync");
        }

        [Test]
        public void Destinations_GetAllAsync_Has_ProducesResponse_Atttribute_With_200_StatusCode()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var sut = new DestinationsController(mediator.Object);
            var expectedStatusCode = 200;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllAsync(It.IsAny<PagingOptions>(), It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public void Destinations_GetAllAsync_Has_ProducesResponse_Atttribute_With_Correct_Return_Type()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var sut = new DestinationsController(mediator.Object);
            var expectedTyped = typeof(IEnumerable<DestinationPreviewDto>);

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllAsync(It.IsAny<PagingOptions>(), It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.Type == expectedTyped).Should().NotBeNull();
        }

        [Test]
        public void Destinations_GetAllAsync_Has_ProducesResponse_Atttribute_With_204_StatusCode()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var sut = new DestinationsController(mediator.Object);
            var expectedStatusCode = 204;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllAsync(It.IsAny<PagingOptions>(), It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public void Destinations_GetAllAsync_Has_ProducesResponse_Atttribute_With_404_StatusCode()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var sut = new DestinationsController(mediator.Object);
            var expectedStatusCode = 404;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllAsync(It.IsAny<PagingOptions>(), It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public void Destinations_GetAllAsync_Has_ProducesResponse_Atttribute_With_400_StatusCode()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var sut = new DestinationsController(mediator.Object);
            var expectedStatusCode = 400;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllAsync(It.IsAny<PagingOptions>(), It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public async Task Destinations_GetByIdAsync_GetDestinationPreviewQuery_Has_Correct_Data()
        {
            // Arrange
            var mediator = new Mock<IMediator>();
            var destinationId = 1;
            var sut = new DestinationsController(mediator.Object);

            // Act
            await sut.GetByIdAsync(destinationId, default(CancellationToken));

            // Assert
            mediator.Verify(x => x.Send(It.Is<GetDestinationPreviewQuery>(d => d.Id == destinationId), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Destinations_GetByIdAsync_Returns_OK_200_Result()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destination);
            var destinationId = 1;
            var sut = new DestinationsController(mockMediator.Object);

            // Act
            var result = await sut.GetByIdAsync(destinationId, default(CancellationToken));

            // Assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject;
            response.StatusCode.Should().Equals(200);
        }

        [Test]
        public async Task Destinations_GetByIdAsync_Returns_NotFound_404_Result()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync((DestinationResponse)null).Verifiable();

            var sut = new DestinationsController(mockMediator.Object);
            var destinationId = 22;

            // Act
            var result = await sut.GetByIdAsync(destinationId, default(CancellationToken));

            // Assert
            var reponse = result.Should().BeOfType<NotFoundObjectResult>().Subject;
            reponse.StatusCode.Should().Equals(404);
        }

        [Test]
        public async Task Destinations_GetByIdAsync_Returns_OK_200_With_Correct_Data()
        {
            // Arranges
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destination);
            var sut = new DestinationsController(mockMediator.Object);
            var destinationId = 1;

            // Act
            var result = await sut.GetByIdAsync(destinationId, default(CancellationToken));

            // Assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject;
            response.StatusCode.Should().Equals(200);
            var destination = response.Value.Should().BeAssignableTo<DestinationResponse>().Subject;
            destination.Data.Id.Should().Be(1);
            destination.Data.Name.Should().Be("Test");
        }

        [Test]
        public async Task Destinations_GetAllAsync_Returns_OK_200_Result()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destinations);
            var sut = new DestinationsController(mockMediator.Object);
            var pagingOptions = new PagingOptions { PageNumber = 1, PageSize = 10 };

            // Act
            var result = await sut.GetAllAsync(pagingOptions, default(CancellationToken));

            // Assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject;
            response.StatusCode.Should().Equals(200);
        }

        [Test]
        public async Task Destinations_GetAllAsync_GetDestinationPreviewQuery_Can_Verify()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var sut = new DestinationsController(mockMediator.Object);
            var pagingOptions = new PagingOptions { PageNumber = 1, PageSize = 10 };

            // Act
            await sut.GetAllAsync(pagingOptions, default(CancellationToken));

            // Assert
            mockMediator.Verify(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Destinations_GetAllAsync_Returns_OK_200_Result_With_Correct_Data()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destinations);

            var sut = new DestinationsController(mockMediator.Object);
            var pagingOptions = new PagingOptions { PageNumber = 1, PageSize = 10 };

            // Act
            var result = await sut.GetAllAsync(pagingOptions, default(CancellationToken));

            // Assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject;
            response.StatusCode.Should().Equals(200);

            var destination = response.Value.Should().BeAssignableTo<PagedDestinationResponse>().Subject;
            destination.Data.Select(x => x.Id == 1).Should().NotBeNull();
            destination.Data.Select(x => x.Name == "Test").Should().NotBeNull();
            destination.Data.Count.Should().Equals(3);
        }
    }
}