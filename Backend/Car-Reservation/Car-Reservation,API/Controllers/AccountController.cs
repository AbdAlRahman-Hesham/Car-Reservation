using Car_Reservation_Domain.Entities.Identity;
using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.DTOs.AccountDtos;
using E_Commerce.DTOs.ErrorResponse;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace E_Commerce.APIs.Controllers;


public class AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, IAuthServices authServices) : BaseApiController
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IAuthServices _authServices = authServices;

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
            return Conflict(new ApiValidationResponse() { Errors = ["Email is used"], StatusCode = StatusCodes.Status409Conflict });
        var user = new User
        {
            FName = registerDto.FName,
            LName = registerDto.LName,
            PicUrl = registerDto.PicUrl,
            Address = registerDto.Address.Adapt<UserAddress>(),
            Email = registerDto.Email,
            UserName = registerDto.Email,
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
            PicUrl = registerDto.PicUrl,
            Address = registerDto.Address,
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




    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }
}
