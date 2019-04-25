namespace Alpha.Travel.WebApi.Mappings
{
    using AutoMapper;
    using Alpha.Travel.WebApi.Models;
    using Alpha.Travel.Application.Destinations.Models;

    public class DestinationMappings : Profile
    {
        public DestinationMappings()
        {
            CreateMap<DestinationPreviewDto, Destination>();
            CreateMap<Destination, DestinationPreviewDto>();
        }
    }
}