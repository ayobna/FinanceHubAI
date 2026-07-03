using FinanceHubAI.Application.Common.Exceptions;
using FluentValidation;
using MediatR;

namespace FinanceHubAI.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(validator =>
                validator.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .GroupBy(
                failure => failure.PropertyName,
                failure => failure.ErrorMessage)
            .ToDictionary(
                group => group.Key,
                group => group.Distinct().ToArray());

        if (failures.Count != 0)
            throw new ApplicationValidationException(failures);

        return await next();
    }
}