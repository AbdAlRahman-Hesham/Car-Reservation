using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.ServicesInterfaces;
using Car_Reservation.DTOs.AccountDtos;
using Car_Reservation.DTOs.ErrorResponse;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;


namespace Car_Reservation.APIs.Controllers;


public class AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthServices authServices, IOptions<CloudinarySettings> cloudinaryConfig) : BaseApiController
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IAuthServices _authServices = authServices;
    private readonly Cloudinary _cloudinary = new Cloudinary(new Account(
            cloudinaryConfig.Value.CloudName,
            cloudinaryConfig.Value.APIKey,
            cloudinaryConfig.Value.APISecret
        ));

    [HttpPost("Login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);
        if (user is null)
            return Unauthorized(new ApiResponse(401));

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            return Unauthorized(new ApiResponse(401));
        var token = await _authServices.CreateToken(user, _userManager);

        return Ok(new UserDto()
        {
            Email = user.Email!,
            FName = user.FName,
            LName = user.LName,
            PicUrl = user.PicUrl,
            Address = user.Address.Adapt<UserAddressDto>(),
            Token = token
        });

    }
    [HttpPost("Register")]
    [ProducesResponseType(typeof(UserDto), 200)]
    [ProducesResponseType(typeof(ApiValidationResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (CheckEmailExists(registerDto.Email).Result.Value)
            return Conflict(new ApiValidationResponse() { Errors = new List<string> { "Email is used" }, StatusCode = StatusCodes.Status409Conflict });

        // Handle the image upload to Cloudinary
        var picUrl = "";
        if (registerDto.Picture != null)
        {
            var uploadResult = await UploadImageToCloudinary(registerDto.Picture);
            if (uploadResult.Error != null)
                return BadRequest(new ApiResponse(400, "Image upload failed: " + uploadResult.Error.Message));

            picUrl = uploadResult.SecureUrl.ToString();
        }

        var user = new User
        {
            FName = registerDto.FName,
            LName = registerDto.LName,
            PicUrl = picUrl, // Set the secure URL from Cloudinary
            Address = registerDto.Address?.Adapt<UserAddress>()!,
            Email = registerDto.Email,
            UserName = registerDto.Email,
            NationalId = registerDto.NationalId,
            PhoneNumber = registerDto.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
            return BadRequest(new ApiResponse(400));

        var token = await _authServices.CreateToken(user, _userManager);
        return Ok(new UserDto()
        {
            FName = registerDto.FName,
            LName = registerDto.LName,
            PicUrl = picUrl,
            Address = registerDto.Address!,
            Email = registerDto.Email,
            Token = token
        });

    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var email = User.FindFirst(ClaimTypes.Email)!.Value;
        var user = await _userManager.FindByEmailAsync(email);
        return Ok(new UserDto()
        {
            Email = user!.Email!,
            FName = user.FName,
            LName = user.LName,
            PicUrl = user.PicUrl,
            Address = user.Address.Adapt<UserAddressDto>(),
            Token = await _authServices.CreateToken(user, _userManager)
        });

    }
    private async Task<ImageUploadResult> UploadImageToCloudinary(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > 0)
        {
            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                        .Width(500).Height(500).Crop("fill").Gravity("face")
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
        }

        return uploadResult;
    }



    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }
}