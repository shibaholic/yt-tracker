using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DataDTOs;
using Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Extensions;

public static class JwtExtension
{
    // extension method for WebApplicationBuilder
    public static void AddMySecretConfiguration(this WebApplicationBuilder builder)
    {
        MySecretConfiguration.Secrets.ApiKey = builder.Configuration.GetSection("Secrets").GetValue<string>("ApiKey") ?? string.Empty;
        MySecretConfiguration.Secrets.JwtPrivateKey = builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
        MySecretConfiguration.Secrets.PasswordSaltKey = builder.Configuration.GetSection("Secrets").GetValue<string>("PasswordSaltKey") ?? string.Empty;
    }
    
    // extension method for WebApplicationBuilder
    public static void AddJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(MySecretConfiguration.Secrets.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        builder.Services.AddAuthorization();
    }
    
    public static string Generate(UserDTO data)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(MySecretConfiguration.Secrets.JwtPrivateKey);
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GenerateClaims(data),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = credentials,
        };
        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    private static ClaimsIdentity GenerateClaims(UserDTO user)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim("Id", user.Id.ToString()));
        ci.AddClaim(new Claim(ClaimTypes.Name, user.Username));
        foreach (var role in user.Roles)
            ci.AddClaim(new Claim(ClaimTypes.Role, role.Name));

        return ci;
    }
}