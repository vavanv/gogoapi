using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Dependency
{
    public sealed class Bootstrapper
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration,
            IEnumerable<IBootstrapperExtension> extensions)
        {
            foreach (var extension in extensions) extension.Initialize(services, configuration);
        }
    }
}