namespace Alpha.Travel.Application.Destinations.QueryHandlers
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using FluentValidation;
    using Microsoft.EntityFrameworkCore;

    using Persistence;
    using Queries;
    using Models;
    using Exceptions;
    using Common.Handlers;

    public class GetDestinationPreviewQueryHandler : BaseValidationHandler<GetDestinationPreviewQuery, DestinationPreviewDto>
    {
        public GetDestinationPreviewQueryHandler(
            AlphaTravelDbContext context,
            IValidator<GetDestinationPreviewQuery> validator)
            : base(context, validator) { }

        public override async Task<DestinationPreviewDto> OnHandle(GetDestinationPreviewQuery request, CancellationToken cancellationToken)
        {
            int id = int.Parse(request.Id);

            var response = await Context.Destinations
                .Select(DestinationPreviewDto.Projection)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);

            if (response == null)
            {
                throw new DestinationNotFoundException();
            }

            return response;
        }
    }
}