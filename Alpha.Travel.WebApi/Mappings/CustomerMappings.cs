namespace Alpha.Travel.WebApi.Mappings
{
    using AutoMapper;
    using Alpha.Travel.WebApi.Models;
    using Alpha.Travel.Application.Customers.Models;

    public class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            CreateMap<CustomerPreviewDto, Customer>();
            CreateMap<Customer, CustomerPreviewDto>();
        }
    }
}