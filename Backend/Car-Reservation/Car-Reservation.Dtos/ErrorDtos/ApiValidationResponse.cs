namespace E_Commerce.DTOs.ErrorResponse;

public class ApiValidationResponse:ApiResponse
{
    public IEnumerable<string> Errors { get; set; }

    public ApiValidationResponse():base(400)
    {
        Errors = new List<string>();
    }
}
