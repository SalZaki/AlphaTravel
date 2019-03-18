namespace Alpha.Travel.WebApi.IntegrationTests.Controller
{
    using Models;
    using Host;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class DestinationsControllerIntegrationTests : CustomWebApplicationFactory<Startup>
    {
        private readonly HttpClient _client;

        public DestinationsControllerIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Test]
        public async Task CanGetPlayers()
        {
            var httpResponse = await _client.GetAsync("/destinations");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<IEnumerable<Destination>>(stringResponse);
            //Assert.Contains(players, p => p.FirstName == "Wayne");
            //Assert.Contains(players, p => p.FirstName == "Mario");
        }
    }
}