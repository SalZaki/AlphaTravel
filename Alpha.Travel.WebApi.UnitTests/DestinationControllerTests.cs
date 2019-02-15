namespace Alpha.Travel.WebApi.UnitTests
{
    using Alpha.Travel.Application.Categories.Models;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using Moq;

    [TestFixture]
    public class DestinationControllerTests
    {
        private IEnumerable<DestinationPreviewDto> _mockDestinations;

        [SetUp]
        public void Setup()
        {
            _mockDestinations =
            new List<DestinationPreviewDto>
            {
                new DestinationPreviewDto
                {
                        Id = 1,
                        Description = "This is a test destination",
                        Name ="Test"
                }
            };
        }
    }
}