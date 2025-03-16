using Car_Reservation.DTOs.ErrorResponse;
using System.Net;
using System.Text.Json;

namespace Car_Reservation.APIs.Middlewares;

public class ExceptionHandlingMiddleware
    (ILogger<ExceptionHandlingMiddleware> logger
    , IHostEnvironment environment):IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly IHostEnvironment _environment = environment;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.ContentType = "appication/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _environment.IsDevelopment() ?
               new ApiExceptionErrorResponse(ex.Message, ex.StackTrace) :
               new ApiExceptionErrorResponse();
            var optiens = new JsonSerializerOptions() { PropertyNamingPolicy =  JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(response,optiens);

            await context.Response.WriteAsync(json);
        }
    }
}
