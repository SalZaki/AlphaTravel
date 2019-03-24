namespace Alpha.Travel.WebApi.UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Application.Models;
    using Application.Destinations.Queries;
    using NUnit.Framework;
    using FluentAssertions;
    using MediatR;
    using Moq;
    using Microsoft.AspNetCore.Mvc;
    using Controllers.V1;
    using Models;
    using Microsoft.Extensions.Options;
    using Application.Common.Models;

    [TestFixture]
    public class DestinationsControllerTest : BaseTest
    {
        [Test]
        public void GetAllAsync_Has_HttpGet_Attribute()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllDestinationsAsync(
                It.IsAny<PagingOptions>(),
                It.IsAny<SortOptions>(),
                It.IsAny<SearchOptions>(),
                It.IsAny<CancellationToken>())).OfType<HttpGetAttribute>().SingleOrDefault();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Name.Should().Be("GetAllDestinationsAsync");
        }

        [Test]
        public void GetAllAsync_Has_ProducesResponse_Atttribute_With_200_StatusCode()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var expectedStatusCode = 200;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllDestinationsAsync(
                It.IsAny<PagingOptions>(),
                It.IsAny<SortOptions>(),
                It.IsAny<SearchOptions>(),
                It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public void GetAllAsync_Has_ProducesResponse_Atttribute_With_Correct_Return_Type()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var expectedTyped = typeof(IEnumerable<DestinationPreviewDto>);

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllDestinationsAsync(
                It.IsAny<PagingOptions>(),
                It.IsAny<SortOptions>(),
                It.IsAny<SearchOptions>(),
                It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.Type == expectedTyped).Should().NotBeNull();
        }

        [Test]
        public void GetAllAsync_Has_ProducesResponse_Atttribute_With_204_StatusCode()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var expectedStatusCode = 204;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllDestinationsAsync(
                It.IsAny<PagingOptions>(),
                It.IsAny<SortOptions>(),
                It.IsAny<SearchOptions>(),
                It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public void GetAllAsync_Has_ProducesResponse_Atttribute_With_404_StatusCode()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var expectedStatusCode = 404;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllDestinationsAsync(
                It.IsAny<PagingOptions>(),
                It.IsAny<SortOptions>(),
                It.IsAny<SearchOptions>(),
                It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public void GetAllAsync_Has_ProducesResponse_Atttribute_With_400_StatusCode()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var expectedStatusCode = 400;

            // Act
            var attributes = sut.GetAttributesOn(x => x.GetAllDestinationsAsync(
                It.IsAny<PagingOptions>(),
                It.IsAny<SortOptions>(),
                It.IsAny<SearchOptions>(),
                It.IsAny<CancellationToken>())).OfType<ProducesResponseTypeAttribute>();

            // Assert
            attributes.Should().NotBeNull();
            attributes.Count().Should().BeGreaterThan(0);
            attributes.Select(x => x.StatusCode == expectedStatusCode).Should().NotBeNull();
        }

        [Test]
        public async Task GetByIdAsync_GetDestinationPreviewQuery_Has_Correct_Data()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var destinationId = "1";

            // Act
            await sut.GetDestinationByIdAsync(destinationId, default(CancellationToken));

            // Assert
            mockMediator.Verify(x => x.Send(It.Is<GetDestinationPreviewQuery>(d => d.Id == destinationId), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task GetByIdAsync_Returns_OK_200_Result()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destination);
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var destinationId = "1";

            // Act
            var result = await sut.GetDestinationByIdAsync(destinationId, default(CancellationToken));

            // Assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject;
            response.StatusCode.Should().Equals(200);
        }

        [Test]
        public async Task GetAllAsync_Returns_OK_200_Result()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destinations);
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);

            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 10
            };

            var sortOptions = new SortOptions
            {
                OrderBy = "Name"
            };

            var searchOptions = new SearchOptions
            {
                Query = "London"
            };

            // Act
            var result = await sut.GetAllDestinationsAsync(pagingOptions, sortOptions, searchOptions, default(CancellationToken));

            // Assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject;
            response.StatusCode.Should().Equals(200);
        }

        [Test]
        public async Task GetAllAsync_GetDestinationsPreviewQuery_Can_Verify()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destinations);
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 10
            };

            var sortOptions = new SortOptions
            {
                OrderBy = "Name"
            };

            var searchOptions = new SearchOptions
            {
                Query = "London"
            };

            // Act
            await sut.GetAllDestinationsAsync(pagingOptions, sortOptions, searchOptions, default(CancellationToken));

            // Assert
            mockMediator.Verify(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task GetAllAsync_Returns_OK_200_Result_With_Correct_Data()
        {
            // Arrange
            var mockMediator = new Mock<IMediator>();
            mockMediator.Setup(x => x.Send(It.IsAny<GetDestinationsPreviewQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(Destinations);
            var mockApiSettings = new Mock<IOptionsSnapshot<ApiSettings>>();
            mockApiSettings.SetupGet(x => x.Value).Returns(ApiSettings);
            var sut = new DestinationsController(mockMediator.Object, mockApiSettings.Object);
            var pagingOptions = new PagingOptions
            {
                PageNumber = 1,
                PageSize = 10
            };

            var sortOptions = new SortOptions
            {
                OrderBy = "Name"
            };

            var searchOptions = new SearchOptions
            {
                Query = "London"
            };

            // Act
            var result = await sut.GetAllDestinationsAsync(pagingOptions, sortOptions, searchOptions, default(CancellationToken));

            // Assert
            var response = result.Should().BeOfType<OkObjectResult>().Subject;
            response.StatusCode.Should().Equals(200);

            var destination = response.Value.Should().BeAssignableTo<PagedResult<DestinationPreviewDto>>().Subject;
            destination.Data.Select(x => x.Id == 1).Should().NotBeNull();
            destination.Data.Select(x => x.Name == "London").Should().NotBeNull();
            destination.Data.Count().Should().Equals(3);
        }
    }
}