namespace Alpha.Travel.Application.Customers.Queries
{
    using MediatR;
    using Models;
    using Common.Queries;
    using System.Collections.Generic;

    public class GetCustomersPreviewQuery : BaseGetPreviewQuery, IRequest<IList<CustomerPreviewDto>>
    {

    }
}