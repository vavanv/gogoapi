using System;

using Framework.Dependency;

using GoGoApi.Mappers;

using Microsoft.Extensions.DependencyInjection;

namespace GoGoApi
{
    public class MapperBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services,
            Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.AddScoped<IStopDetailMapper, StopDetailMapper>();
        }
    }
}