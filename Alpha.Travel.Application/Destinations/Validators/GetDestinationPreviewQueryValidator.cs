namespace Alpha.Travel.Application.Destinations.Validators
{
    using FluentValidation;
    using Queries;

    public class GetDestinationPreviewQueryValidator : AbstractValidator<GetDestinationPreviewQuery>
    {
        public GetDestinationPreviewQueryValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty()
                .WithMessage("Destination id cannot be emplty");
        }
    }
}