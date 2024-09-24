using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Extentions;

public static class IdentityServiceExtentions
{
    public static IServiceCollection AddAIdentityServices(this IServiceCollection services,
        IConfiguration config)
    {

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var tokenkey = config["TokenKey"] ?? throw new Exception("TK not found");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;

    }
}