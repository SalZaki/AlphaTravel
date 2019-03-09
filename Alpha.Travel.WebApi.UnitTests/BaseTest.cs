namespace Alpha.Travel.WebApi.UnitTests
{
    using Application.Destinations.Models;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    [TestFixture]
    public abstract class BaseTest
    {
        public DestinationResponse Destination { get; private set; }

        public PagedDestinationResponse Destinations { get; private set; }

        [SetUp]
        public void Setup()
        {
            Destination = new DestinationResponse
            {
                Data = new DestinationPreviewDto
                {
                    Id = 1,
                    Description = "This is a test destination",
                    Name = "Test"
                }
            };

            Destinations = new PagedDestinationResponse
            {
                Data = new List<DestinationPreviewDto>() {
                    new DestinationPreviewDto
                    {
                        Id = 1,
                        Description = "This is a test destination",
                        Name = "Test1"
                    },

                    new DestinationPreviewDto
                    {
                        Id = 2,
                        Description = "This is a test 2 destination",
                        Name = "Test2"
                    },

                    new DestinationPreviewDto
                    {
                        Id = 3,
                        Description = "This is a test 3 destination",
                        Name = "Test3"
                    }
                },
                TotalPages = 1,
                TotalCount = 3,
                PageIndex = 1,
                PageSize = 10
            };
        }
    }
}