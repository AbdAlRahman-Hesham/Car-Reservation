using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Car_Reservation_Domain.Entities.Identity;
using Car_Reservation_Domain.ServicesInterfaces;
using Car_Reservation.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Car_Reservation_API.Extension;

internal static class AppIdentityAndJwtAuthenticationExtensionHelpers
{

    public static IServiceCollection AddIdentityAndJwtAuthenticationServices(this IServiceCollection service, IConfiguration configuration)
    {

        service.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<CarRentDbContext>();

        service.AddScoped<IAuthServices, AuthServices>();

        service.AddAuthentication(op =>
        {
            op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            op.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]!)),
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:Lifetime"]!))

            };
        }).AddCookie("Forbidden", options =>
        {
            options.AccessDeniedPath = "/errors/403";
        }); ;
        return service;
    }
}