using bms.Domain.Common;
using FluentValidation;
using MediatR;
using Serilog;

namespace bms.Application.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TRespond>(
        IEnumerable<IValidator<TRequest>> validators,
        ILogger logger)
        : IPipelineBehavior<TRequest, TRespond>
        where TRequest : IRequest<TRespond>

    {
        public async Task<TRespond> Handle(
            TRequest request,
            RequestHandlerDelegate<TRespond> next,
            CancellationToken cancellationToken)
        {
            if (!validators.Any())
            {
                logger.Information("No validations found for {RequestType}",
                    typeof(TRequest).Name);
                return await next(cancellationToken);
            }

            //create a validation context for the request
            var context = new ValidationContext<TRequest>(request);

            //run all validators asynchronously
            var validationResults = await Task.WhenAll(
                validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .Where(result => result.Errors.Any())
                .SelectMany(result => result.Errors)
                .ToList();
        

            if (!failures.Any())
            {
                return await next(cancellationToken);
            }

            var combinedMessage = string.Join("; ", failures.Select(f => f.ErrorMessage));
            logger.Warning("Validation failed: {Message}", combinedMessage);

            // If response is Result<T>, return Result<T>.Failure(...)
            if (typeof(TRespond).IsGenericType &&
                typeof(TRespond).GetGenericTypeDefinition() == typeof(Result<>))
            {
                var resultType = typeof(TRespond).GetGenericArguments()[0];
                var failureResult = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(Result<object>.Failure))!
                    .Invoke(null, [new Error(combinedMessage)]);

                return (TRespond)failureResult!;
            }

            // if not Result<T>, throw a ValidationException
            throw new ValidationException(failures);
        }
    }
}
