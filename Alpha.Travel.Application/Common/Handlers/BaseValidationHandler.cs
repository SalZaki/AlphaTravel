namespace Alpha.Travel.Application.Common.Handlers
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Persistence;
    using FluentValidation;
    using MediatR;

    public abstract class BaseValidationHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public AlphaTravelDbContext Context { get; }

        public IValidator<TRequest> Validator { get; }

        public BaseValidationHandler(AlphaTravelDbContext context, IValidator<TRequest> validator)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Validator = validator;
        }

        public abstract Task<TResponse> OnHandle(TRequest request, CancellationToken cancellationToken);

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (Validator != null)
            {
                var result = (await Task.WhenAll(Validator.ValidateAsync(request)))
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