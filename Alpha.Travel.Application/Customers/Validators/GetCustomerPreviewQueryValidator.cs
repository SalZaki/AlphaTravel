namespace Alpha.Travel.Application.Customers.Validators
{
    using Common.Validators;
    using Common.Enums;
    using FluentValidation;
    using Queries;

    public class GetCustomerPreviewQueryValidator : AbstractValidator<GetCustomerPreviewQuery>
    {
        public GetCustomerPreviewQueryValidator()
        {
            RuleFor(x => x.Id)
                .IsValidIntId()
                .WithErrorCode(Error.InvalidDestinationId.ToString());
        }
    }
}