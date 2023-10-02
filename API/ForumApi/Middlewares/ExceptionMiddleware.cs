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
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"{ex.Message} is empty");
            }
            catch(BadRequestException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch(ForbiddenException ex)
            {
                context.Response.StatusCode = 403;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch(NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync(ex.Message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Internal server error.");
            }
        }
    }
}