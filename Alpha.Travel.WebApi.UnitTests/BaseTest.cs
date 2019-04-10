namespace Alpha.Travel.WebApi.UnitTests
{
    using System;
    using System.Net.Http;
    using System.Reflection;
    using System.Collections.Generic;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.AspNetCore.Hosting;
    using NUnit.Framework;

    using Host;
    using AutoMapper;
    using Application.Customers.Models;
    using Application.Customers.Mappings;
    using Application.Destinations.Models;
    using Application.Destinations.Mappings;
    using Alpha.Travel.WebApi.Models;
    using Newtonsoft.Json;
    using System.IO;

    public abstract class BaseTest
    {
        private static readonly string FixtureDir = "../../../fixtures/";

        public DestinationPreviewDto Destination { get; private set; }

        public IList<DestinationPreviewDto> Destinations { get; private set; }

        public CustomerPreviewDto Customer { get; private set; }

        public IList<CustomerPreviewDto> Customers { get; private set; }

        public PagedResponse<Customer> PagedCustomers { get; private set; }

        public PagedResponse<Destination> PagedDestinations { get; private set; }

        public ApiSettings ApiSettings { get; private set; }

        public HttpClient Client { get; private set; }

        public TestServer Server { get; private set; }

        public IMapper Mapper { get; private set; }

        [TearDown]
        public virtual void Cleanup() { }

        [SetUp]
        public virtual void Init()
        {
            ApiSettings = new ApiSettings
            {
                ApiDocumentationUrl = "https://www.alphatravel.co.uk/v{VERSION}/documentation/",
                DefaultPageNumber = 1,
                DefaultPageSize = 20
            };

            Destination = new DestinationPreviewDto
            {
                Id = 1,
                Description = "This is a test destination",
                Name = "London"
            };

            Customer = new CustomerPreviewDto
            {
                Id = 1,
                Email = "test@test.com",
                Firstname = "Jason",
                Surname = "Thomson",
                Password = "Password123"
            };

            Destinations = new List<DestinationPreviewDto>{
                    new DestinationPreviewDto
                    {
                        Id = 1,
                        Description = "This is a test destination",
                        Name = "London"
                    },
                    new DestinationPreviewDto
                    {
                        Id = 2,
                        Description = "This is a test 2 destination",
                        Name = "Paris"
                    },
                    new DestinationPreviewDto
                    {
                        Id = 3,
                        Description = "This is a test 3 destination",
                        Name = "New York"
                    }
            };

            Customers = new List<CustomerPreviewDto>{
                    new CustomerPreviewDto
                    {
                        Id = 1,
                        Email = "test@test.com",
                        Firstname = "Jason",
                        Surname = "Thomson",
                        Password = "Password123"
                    },
                new CustomerPreviewDto
                {
                    Id = 2,
                    Email = "test@test.com",
                    Firstname = "Jason",
                    Surname = "Thomson",
                    Password = "Password123"
                },
                new CustomerPreviewDto
                {
                    Id = 3,
                    Email = "test@test.com",
                    Firstname = "Jason",
                    Surname = "Thomson",
                    Password = "Password123"
                }
            };
            PagedCustomers = GetFixture<PagedResponse<Customer>>("customers.json");
            PagedDestinations = GetFixture<PagedResponse<Destination>>("destinations.json");
            Server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
            Client.Timeout = TimeSpan.FromMinutes(20);
            Mapper = CreateMapper();
        }

        private static T GetFixture<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(Path.Combine(FixtureDir, file)));
        }

        private IMapper CreateMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(typeof(CustomerMappings).GetTypeInfo().Assembly);
                cfg.AddProfiles(typeof(DestinationMappings).GetTypeInfo().Assembly);
            });

            return mapperConfig.CreateMapper();
        }
    }
}