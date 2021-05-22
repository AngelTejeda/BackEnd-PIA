using API_Rest.Middleware;
using Microsoft.AspNetCore.Builder;

namespace API_Rest.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        // Indica a la aplicación qué middleware utilizar en caso de excepciones.
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
