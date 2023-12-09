using System.Text.Json;
using ForumApi.Exceptions;

namespace ForumApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(ArgumentNullException ex)
            {
                await HandleError(context, 400, $"{ex.Message} is empty");
            }
            catch(BadRequestException ex)
            {
                await HandleError(context, 400, ex.Message);
            }
            catch(NotFoundException ex)
            {
                await HandleError(context, 404, ex.Message);
            }
            catch(ForbiddenException ex)
            {
                await HandleError(context, 403, ex.Message);
            }
            catch(FluentValidation.ValidationException ex)
            {
                var errorsObj = new { errors = ex.Errors.Select(e => new {e.PropertyName, e.ErrorMessage}) };

                await HandleError(context, 400, JsonSerializer.Serialize(errorsObj));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleError(context, 500, "Internal server error.");
            }
        }

        private async Task HandleError(HttpContext context, int statusCode, string message)
        {
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(message);
        }
    }
}