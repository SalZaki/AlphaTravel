namespace Alpha.Travel.WebApi.ClientSDK
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class AlphaTravelClient
    {
        private readonly string _authToken;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly int _maxRequestTries;

        public AlphaTravelClient(string apiBaseAddress, string authToken, TimeSpan? timeOut = null, int? maxRequestTries = null)
        {
#if (NET45 || NET451 || NET452 || NET46 || NET461 || NET462)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif
            _httpClient = new HttpClient
            {
                Timeout = timeOut ?? new TimeSpan(0, 0, 10),
                BaseAddress = new Uri(apiBaseAddress)
            };
            _httpClient.DefaultRequestHeaders.Add("X-AlphaTravel-Client", ".NET/" + typeof(AlphaTravelClient).GetTypeInfo().Assembly.GetName().Version.ToString());
            _maxRequestTries = maxRequestTries ?? 3;
            _authToken = authToken;
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }
    }
}