namespace Alpha.Travel.WebApi.IntegrationTests.Controller
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using Newtonsoft.Json;
    using FluentAssertions;
    using Models;

    [TestFixture]
    public sealed class DestinationsControllerIntegrationTests : AlphaTravelTestWebApplicationFactory
    {
        public const string _destinationsEndpoint = "/api/v1/destinations";

        [Test]
        public async Task GetAsync_All_Destinations_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync(_destinationsEndpoint, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_All_Destinations_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync(_destinationsEndpoint, CancellationToken.None);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var destinations = JsonConvert.DeserializeObject<PagedResponse<Destination>>(stringResponse);

            // Assert
            Assert.Contains(destinations.Data.Where(d => d.Name == "London").FirstOrDefault(), destinations.Data.ToList());
        }

        [Test]
        public async Task GetAsync_Paged_Destinations_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_destinationsEndpoint}/?pageSize=50&pageNumber=1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_Paged_Destinations_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_destinationsEndpoint}/?pageSize=50&pageNumber=1", CancellationToken.None);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var destinations = JsonConvert.DeserializeObject<PagedResponse<Destination>>(stringResponse);

            // Assert
            destinations.Pagination.PageNumber.Should().Equals(1);
            destinations.Pagination.PageSize.Should().Equals(50);
        }

        [Test]
        public async Task GetAsync_Single_Destination_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_destinationsEndpoint}/1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_Single_Destination_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_destinationsEndpoint}/1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var destination = JsonConvert.DeserializeObject<Response<Destination>>(stringResponse);

            // Assert
            Assert.AreEqual(destination.Data.Name, "London");
        }

        [Test]
        public async Task PostAsync_Destination_Returns_Created()
        {
            // Arrange
            var entity = new Destination
            {
                Id = 11,
                Description = "Description",
                Name = "Name",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "CreatedOn"
            };
            var content = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PostAsync(_destinationsEndpoint, content, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task PutAsync_Destination_Returns_NoContent()
        {
            // Arrange
            var entity = new Destination
            {
                Id = 1,
                Description = "Description",
                Name = "Name",
                CreatedOn = DateTime.UtcNow,
                CreatedBy = "CreatedOn"
            };
            var content = new StringContent(JsonConvert.SerializeObject(entity), System.Text.Encoding.UTF8, "application/json");

            // Act
            var response = await Client.PutAsync($"{_destinationsEndpoint}/1", content, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task DeleteAsync_Destination_Returns_NoContent()
        {
            // Arrange & Act
            var response = await Client.DeleteAsync($"{_destinationsEndpoint}/4", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}