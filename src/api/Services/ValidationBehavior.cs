using FluentValidation;
using MediatR;

namespace TEcomerc.Api.Services;


sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var results = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = results
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();

        return failures.Count == 0 ? await next(cancellationToken)
            : throw new ValidationException(failures);
    }
}
