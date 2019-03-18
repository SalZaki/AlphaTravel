namespace Alpha.Travel.Application.Customers.Queries
{
    using Models;
    using MediatR;

    public class GetCustomerPreviewQuery : IRequest<CustomerPreviewDto>
    {
        public string Id { get; set; }
    }
}