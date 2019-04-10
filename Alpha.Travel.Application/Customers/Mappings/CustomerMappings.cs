namespace Alpha.Travel.Application.Customers.Mappings
{
    using AutoMapper;
    using Models;
    using Domain.Entities;

    public class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            CreateMap<Customer, CustomerPreviewDto>().ReverseMap();
        }
    }
}