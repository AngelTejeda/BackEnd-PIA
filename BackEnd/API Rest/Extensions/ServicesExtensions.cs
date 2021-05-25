using Core.BackEnd;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API_Rest.Extensions
{
    public static class ServicesExtensions
    {
        // ExtensionMethod que agrega autenticación mediante Token JWT.
        // La autenticación valida el issuer, la audiencia, el tiempo de expiración y la Signing Key del Token.
        public static void AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    JwtParamethers jwt = new();

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt.GetIssuer(),
                        ValidAudience = jwt.GetIssuer(),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.GetSecretKey()))
                    };
                });
        }
    }
}
