namespace Alpha.Travel.WebApi.Infrastructure
{
    using Models;
    using AutoMapper;
    using Application.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DestinationPreviewDto, Destination>()
                .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDTO => cDTO.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(cDTO => cDTO.CreatedBy, opt => opt.MapFrom(c => c.CreatedBy))
                .ForMember(cDTO => cDTO.CreatedOn, opt => opt.MapFrom(c => c.CreatedOn))
                .ForMember(cDTO => cDTO.ModifiedBy, opt => opt.MapFrom(c => c.ModifiedBy))
                .ForMember(cDTO => cDTO.ModifiedOn, opt => opt.MapFrom(c => c.ModifiedOn))
                .ForMember(cDTO => cDTO.Self, opt => opt.MapFrom(c => Link.To(nameof(Controllers.V1.DestinationsController.GetByIdAsync), c.Id)));

            CreateMap<CustomerPreviewDto, Customer>()
                .ForMember(cDTO => cDTO.Id, opt => opt.MapFrom(c => c.Id))
                .ForMember(cDTO => cDTO.Firstname, opt => opt.MapFrom(c => c.Firstname))
                .ForMember(cDTO => cDTO.Surname, opt => opt.MapFrom(c => c.Surname))
                .ForMember(cDTO => cDTO.Password, opt => opt.MapFrom(c => c.Password))
                .ForMember(cDTO => cDTO.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(cDTO => cDTO.CreatedOn, opt => opt.MapFrom(c => c.CreatedOn))
                .ForMember(cDTO => cDTO.ModifiedBy, opt => opt.MapFrom(c => c.ModifiedBy))
                .ForMember(cDTO => cDTO.ModifiedOn, opt => opt.MapFrom(c => c.ModifiedOn))
                .ForMember(cDTO => cDTO.Self, opt => opt.MapFrom(c => Link.To(nameof(Controllers.V1.CustomersController.GetCustomerByIdAsync), c.Id)));
        }
    }
}