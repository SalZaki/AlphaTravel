namespace Alpha.Travel.Application.Customers.Queries
{
    using Models;
    using MediatR;

    public class GetCustomerPreviewQuery : IRequest<CustomerPreviewDto>
    {
        public int Id { get; set; }
    }
}