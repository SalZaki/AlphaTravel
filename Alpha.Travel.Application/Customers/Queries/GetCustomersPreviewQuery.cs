namespace Alpha.Travel.Application.Customers.Queries
{
    using MediatR;
    using Models;

    public class GetCustomersPreviewQuery : IRequest<PagedResults<CustomerPreviewDto>>
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public string OrderBy { get; set; }

        public string Query { get; set; }
    }
}