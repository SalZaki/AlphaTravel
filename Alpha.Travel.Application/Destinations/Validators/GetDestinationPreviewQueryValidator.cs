namespace Alpha.Travel.Application.Destinations.Validators
{
    using Common.Validators;
    using Common.Enums;
    using FluentValidation;
    using Queries;

    public class GetDestinationPreviewQueryValidator : AbstractValidator<GetDestinationPreviewQuery>
    {
        public GetDestinationPreviewQueryValidator()
        {
            RuleFor(x => x.Id)
                .IsValidIntId()
                .WithErrorCode(Error.InvalidDestinationId.ToString());
        }
    }
}