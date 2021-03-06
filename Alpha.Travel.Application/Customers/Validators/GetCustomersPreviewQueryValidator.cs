﻿namespace Alpha.Travel.Application.Customers.Validators
{
    using Common.Validators;
    using Common.Enums;
    using FluentValidation;
    using Queries;

    public class GetCustomersPreviewQueryValidator : AbstractValidator<GetCustomersPreviewQuery>
    {
        public GetCustomersPreviewQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .IsValidIntId()
                .WithErrorCode(Error.InvalidPageNumber.ToString())
                .WithMessage("Page number must be positive integer starting from 0.");

            RuleFor(x => x.PageSize)
                .IsValidIntId()
                .WithErrorCode(Error.InvalidPageSize.ToString())
                .InclusiveBetween(1, 100)
                .WithMessage("Page size must be greater than 0 and less than 100.");
        }
    }
}