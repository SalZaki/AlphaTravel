namespace Alpha.Travel.WebApi.IntegrationTests.Controller
{
    using System.Net;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using Newtonsoft.Json;
    using FluentAssertions;
    using ClientSDK.Models.Response;

    [TestFixture]
    public sealed class DestinationsControllerIntegrationTests : AlphaTravelTestWebApplicationFactory
    {
        public const string _destinationsEndpoint = "/api/v1/destinations";

        [Test]
        public async Task GetAsync_All_Destionations_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync(_destinationsEndpoint, CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_All_Destionations_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync(_destinationsEndpoint, CancellationToken.None);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var destinations = JsonConvert.DeserializeObject<PagedResult<Destination>>(stringResponse);

            // Assert
            Assert.Contains(destinations.Data.Where(d => d.Name == "London").FirstOrDefault(), destinations.Data.ToList());
        }

        [Test]
        public async Task GetAsync_Single_Destionation_Returns_OK()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_destinationsEndpoint}/1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task GetAsync_Single_Destionation_Returns_OK_With_Correct_Data()
        {
            // Arrange & Act 
            var response = await Client.GetAsync($"{_destinationsEndpoint}/1", CancellationToken.None);
            response.EnsureSuccessStatusCode();

            var stringResponse = await response.Content.ReadAsStringAsync();
            var destination = JsonConvert.DeserializeObject<Destination>(stringResponse);

            // Assert
            Assert.AreEqual(destination.Name, "London");
        }
    }
}