using Car_Reservation.Repository.Contexts.CarRentContext.Data;
using Car_Reservation_Domain.Entities.Identity;
using E_Commerce.Domain.ServicesInterfaces;
using E_Commerce.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Car_Reservation_API.Extension
{
    internal static class AppIdentityAndJwtAuthenticationExtensionHelpers
    {

        public static IServiceCollection AddIdentityAndJwtAuthenticationServices(this IServiceCollection service, IConfiguration configuration)
        {

            service.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CarRentContext>();

            service.AddScoped<IAuthServices, AuthServices>();

            service.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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
            });
            return service;
        }
    }
}