using FluentValidation;
using ILogger = Serilog.ILogger;

namespace bmsAPI.Middleware
{
    public class GlobalExceptionHandlingMiddleware(ILogger logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException)
            {
                logger.Error("Validation error occurred while processing the request.");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "An unhandled exception occurred while processing the request.");
                throw;
            }
        }
    }
}
