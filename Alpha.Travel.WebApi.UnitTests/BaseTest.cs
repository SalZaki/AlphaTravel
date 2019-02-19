namespace Alpha.Travel.WebApi.UnitTests
{
    using Application.Destinations.Models;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public abstract class BaseTest
    {
        public DestinationPreviewDto Destination { get; private set; }

        public List<DestinationPreviewDto> Destinations { get; private set; }

        [SetUp]
        public void Setup()
        {
            Destination = new DestinationPreviewDto
            {
                Id = 1,
                Description = "This is a test destination",
                Name = "Test"
            };

            Destinations = new List<DestinationPreviewDto>()
            {
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
            };
        }
    }
}
