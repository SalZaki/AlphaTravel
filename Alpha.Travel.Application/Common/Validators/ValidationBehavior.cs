namespace Alpha.Travel.Application.Common.Validators
{
    using FluentValidation;
    using MediatR;

    public class ValidationBehavior<TRequest, TResponse> : ValidationBehaviorBase<TRequest, TResponse>
    {
        public ValidationBehavior(IValidator<TRequest> validator) : base(validator) { }
    }

    public class ValidationBehavior<TRequest> : ValidationBehaviorBase<TRequest, Unit>
    {
        public ValidationBehavior(IValidator<TRequest> validator) : base(validator) { }
    }
}