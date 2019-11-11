using System;

using Framework.Dependency;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services.Services.Cache;

namespace Services
{
    public class ServicesBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICacheService, CacheService>();
        }
    }
}