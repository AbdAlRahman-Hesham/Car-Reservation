using System.Net;

namespace Car_Reservation.DTOs.ErrorResponse;

public class ApiExceptionErrorResponse:ApiResponse
{
    public string? Detailes { get; set; }
    public ApiExceptionErrorResponse(string? massage =null,string? detailes = null)
        :base((int)HttpStatusCode.InternalServerError,massage)
    {
        Detailes = detailes?? string.Empty;
    }


 }
