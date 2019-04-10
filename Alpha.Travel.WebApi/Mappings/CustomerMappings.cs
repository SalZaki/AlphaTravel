namespace Alpha.Travel.WebApi.Mappings
{
    using Alpha.Travel.Application.Customers.Models;
    using Alpha.Travel.WebApi.Models;
    using AutoMapper;

    public class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            CreateMap<Customer, CustomerPreviewDto>().ReverseMap();
        }
    }
}