using Lender.API.Application.Notifify;
using Lender.API.Helper;
using Lender.API.Models.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Lender.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<NotificationContext>();
            services.AddScoped<IEntityValidator, EntityValidator>();

            return services;
        }
    }
}
