using System.Text.Json;
using Disney.Exceptions;

namespace Disney.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (ex)
                {
                    case AppException e:
                    case ArgumentOutOfRangeException er:
                    case ArgumentNullException err:
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        break;

                    case KeyNotFoundException e:
                    case ObjectNotFoundException er:
                        response.StatusCode = StatusCodes.Status404NotFound;
                        break;

                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = ex?.Message, trace = ex?.StackTrace, success = false });
                await response.WriteAsync(result);
            }
        }
    }
}