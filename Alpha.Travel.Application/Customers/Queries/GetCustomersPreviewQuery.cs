namespace Alpha.Travel.Application.Customers.Queries
{
    using MediatR;
    using Models;
    using Common.Models;
    using Common.Queries;

    public class GetCustomersPreviewQuery : BaseGetPreviewQuery, IRequest<PagedResult<CustomerPreviewDto>> { }
}