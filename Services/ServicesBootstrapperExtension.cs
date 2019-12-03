using System;

using Framework.Dependency;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Services.DbContextFactory;
using Services.Repository;
using Services.Services.Cache;
using Services.Services.Shape;
using Services.Services.Stop;
using Services.UnitOfWork;

namespace Services
{
    public class ServicesBootstrapperExtension : IBootstrapperExtension
    {
        public void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IStopService, StopService>();
            services.AddScoped<IShapeService, ShapeService>();

            services.AddScoped<IGoGoContextFactory, GoGoContextFactory>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        }
    }
}