namespace Alpha.Travel.Application.Common.Validators
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentValidation;
    using MediatR;

    public abstract class ValidationBehaviorBase<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehaviorBase(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToke, RequestHandlerDelegate<TResponse> next)
        {
            if (_validator == null)
            {
                return next();
            }

            var context = new ValidationContext(request);

            var failures = _validator.Validate(context).Errors;

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}