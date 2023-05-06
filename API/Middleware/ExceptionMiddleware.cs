using API.Controllers;
using API.Errors;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _next = next;
            _environment = environment;
        }

        
        public async Task TaskAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var responce = _environment.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiException((int)HttpStatusCode.InternalServerError);
              
                var options = new JsonSerializerOptions { PropertyNamingPolicy= JsonNamingPolicy.CamelCase };
               
                var json = JsonSerializer.Serialize(responce, options);
                await context.Response.WriteAsync(json);
                
            }
        }
    }
}
