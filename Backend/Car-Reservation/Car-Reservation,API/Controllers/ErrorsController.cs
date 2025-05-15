using Car_Reservation.DTOs.ErrorResponse;
using Microsoft.AspNetCore.Mvc;

namespace Car_Reservation.APIs.Controllers;

[Route("errors/{code}")]
[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    public ActionResult Error(int code) {
        return code switch
        {
            400 => BadRequest(new ApiResponse(code, "A bad request, you have made")),
            401 => Unauthorized(new ApiResponse(code, "Unauthorized")),
            404 => NotFound(new ApiResponse(code, "Resource not found")),
            500 => StatusCode(500, new ApiResponse(code, "An internal server error occurred")),
            _ => StatusCode(code, new ApiResponse(code))
        };

    }

}
