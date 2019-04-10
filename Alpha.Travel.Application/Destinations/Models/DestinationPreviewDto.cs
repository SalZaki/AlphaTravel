namespace Alpha.Travel.Application.Destinations.Models
{
    using Common.Models;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DestinationPreviewDto : BaseDto
    {
        public DestinationPreviewDto() { }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}