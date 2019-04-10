namespace Alpha.Travel.Application.Destinations.Mappings
{
    using AutoMapper;
    using Models;
    using Domain.Entities;

    public class DestinationMappings : Profile
    {
        public DestinationMappings()
        {
            CreateMap<Destination, DestinationPreviewDto>().ReverseMap();
        }
    }
}