using Car_Reservation.DTOs.ErrorResponse;
using Microsoft.AspNetCore.Mvc;

namespace Car_Reservation_API.Extension
{
    internal static class AppApiErrorExtensionHelpers
    {

        public static IServiceCollection AddApiErrorServices(this IServiceCollection service)
        {

            service.Configure<ApiBehaviorOptions>(
                op =>
                {
                    op.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState.Where(x => x.Value!.Errors.Count > 0)
                                                              .SelectMany(x => x.Value!.Errors)
                                                              .Select(x => x.ErrorMessage).ToList();

                        var validationErrorResponse = new ApiValidationResponse() { Errors = errors };

                        return new BadRequestObjectResult(validationErrorResponse);
                    };
                }

                );

            return service;
        }
    }
}