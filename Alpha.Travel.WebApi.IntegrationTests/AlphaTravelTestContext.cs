namespace Alpha.Travel.WebApi.IntegrationTests
{
    using System;
    using System.Net.Http;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using NUnit.Framework;

    [SetUpFixture]
    public abstract class AlphaTravelTestWebApplicationFactory : WebApplicationFactory
    {
        private TimeSpan _defaultTimeout = new TimeSpan(0, 0, 10);

        private const string _apiBaseAddress = "http://localhost:49404";

        public int MaxRequestTries { get; private set; }

        public HttpClient Client { get; private set; }

        public JsonSerializerSettings SerializerSettings { get; private set; }

        [OneTimeSetUp]
        public void Init()
        {
            Client = CreateClient();
            Client.Timeout = _defaultTimeout;
            Client.BaseAddress = new Uri(_apiBaseAddress);
            MaxRequestTries = 3;
            SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [OneTimeTearDown]
        public void Clear()
        {
            Dispose();
        }
    }
}