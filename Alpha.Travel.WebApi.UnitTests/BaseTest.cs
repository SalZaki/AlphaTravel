namespace Alpha.Travel.WebApi.UnitTests
{
    using System;
    using System.Net.Http;
    using System.Collections.Generic;

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.AspNetCore.Hosting;
    using NUnit.Framework;

    using Host;
    using Application.Models;
    using Application.Common.Models;

    public abstract class BaseTest
    {
        public DestinationPreviewDto Destination { get; private set; }

        public PagedResult<DestinationPreviewDto> Destinations { get; private set; }

        public CustomerPreviewDto Customer { get; private set; }

        public PagedResult<CustomerPreviewDto> Customers { get; private set; }

        public ApiSettings ApiSettings { get; private set; }

        public HttpClient Client { get; private set; }

        public TestServer Server { get; private set; }

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

            Destinations = new PagedResult<DestinationPreviewDto>
            {
                Data = new List<DestinationPreviewDto>{
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
                }
            };

            Customers = new PagedResult<CustomerPreviewDto>
            {
                Data = new List<CustomerPreviewDto>{
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
               }
            };
            Server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            Client = Server.CreateClient();
            Client.Timeout = TimeSpan.FromMinutes(20);
        }
    }
}