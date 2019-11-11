using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Dependency
{
    public interface IBootstrapperExtension
    {
        void Initialize(IServiceCollection services, IConfiguration configuration);
    }
}