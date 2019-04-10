namespace Alpha.Travel.Application.Destinations.QueryHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;

    using FluentValidation;
    using Microsoft.EntityFrameworkCore;

    using Persistence;
    using Queries;
    using Models;
    using Exceptions;
    using Common.Handlers;
    using Domain.Entities;

    public class GetDestinationPreviewQueryHandler : BaseValidationHandler<GetDestinationPreviewQuery, DestinationPreviewDto>
    {
        private readonly IMapper _mapper;

        public GetDestinationPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetDestinationPreviewQuery> validator,
            IMapper mapper)
            : base(context, validator)
        {
            _mapper = mapper;
        }

        public override async Task<DestinationPreviewDto> OnHandle(GetDestinationPreviewQuery request, CancellationToken cancellationToken)
        {
            var destination = null as Destination;
            try
            {
                destination = await Context.Destinations.SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }

            if (destination == null)
            {
                throw new DestinationNotFoundException();
            }

            return _mapper.Map<DestinationPreviewDto>(destination);
        }
    }
}