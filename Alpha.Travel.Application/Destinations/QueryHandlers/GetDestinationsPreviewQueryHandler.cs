namespace Alpha.Travel.Application.Destinations.QueryHandlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq.Dynamic.Core;
    using System.Collections.Generic;

    using MediatR;
    using Models;
    using Persistence;
    using Queries;
    using Domain.Entities.Destination;
    using Microsoft.EntityFrameworkCore;
    using FluentValidation;
    using AutoMapper;

    public class GetDestinationsPreviewQueryHandler : ValidationHandler<GetDestinationsPreviewQuery, PagedDestinationResponse>
    {
        public GetDestinationsPreviewQueryHandler(AlphaTravelDbContext context, IMapper mapper, IValidator<GetDestinationsPreviewQuery> validator) : base(context, mapper, null)
        {

        }

        public override async Task<PagedDestinationResponse> OnHandle(GetDestinationsPreviewQuery request, CancellationToken cancellationToken)
        {
            var destinations = Context.Destinations.AsQueryable().OrderBy(request.OrderBy, request.IsDescending()) as IQueryable<Destination>;

            if (request.HasQuery())
            {
                destinations = destinations
                    .Where(x => x.Name.ToLowerInvariant()
                    .Contains(request.Query.ToLowerInvariant()));
            }

            var response = new PagedDestinationResponse
            {
                Data = await destinations
                .Select(DestinationPreviewDto.Projection)
                .Skip(request.PageNumber * (request.PageSize - 1))
                .Take(request.PageNumber)
                .ToListAsync(cancellationToken),
                PageSize = request.PageSize,
                PageIndex = request.PageNumber
            };

            response.TotalPages = response.Data.Count / response.PageSize;
            response.TotalCount = response.Data.Count;
            return response;
        }
    }

    //public class RestGetListRequest<TEntity, TGetModel> : IRequest<object> where TEntity : BaseEntity where TGetModel : IGetModel
    //{
    //    public int PageNumber { get; set; }
    //    public int NumberOfRecords { get; set; }
    //    public bool UsePaging { get; set; }
    //}

    public abstract class ValidationHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public AlphaTravelDbContext Context { get; }

        public IMapper Mapper { get; }

        public IEnumerable<IValidator<TRequest>> Validators { get; }

        public ValidationHandler(AlphaTravelDbContext context, IMapper mapper, IEnumerable<IValidator<TRequest>> validators)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            Validators = validators;
        }

        public abstract Task<TResponse> OnHandle(TRequest request, CancellationToken cancellationToken);

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (Validators != null)
            {
                var result = (await Task.WhenAll(Validators
                    .Where(v => v != null)
                    .Select(v => v.ValidateAsync(request))))
                    .SelectMany(v => v.Errors);

                if (result.Any())
                {
                    throw new ValidationException(result);
                }
            }

            return await OnHandle(request, cancellationToken);
        }
    }
}