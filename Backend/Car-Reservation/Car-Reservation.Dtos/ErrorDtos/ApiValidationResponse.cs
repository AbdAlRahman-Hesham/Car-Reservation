namespace E_Commerce.DTOs.ErrorResponse;

public class ApiValidationResponse:ApiResponse
{
    /// <summary>
    /// {
    ///     statusCode:401,
    ///     errorMessage:.................,
    ///     errors:[
    ///     "....................",
    ///     "....................",
    ///     ]
    /// }
    /// </summary>
    public IEnumerable<string> Errors { get; set; }

    public ApiValidationResponse():base(400)
    {
        Errors = new List<string>();
    }
}
