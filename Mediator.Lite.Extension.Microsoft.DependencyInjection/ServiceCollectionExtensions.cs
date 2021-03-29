using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Implementation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mediator.Lite.Extension.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMediatorLiteHandlers(this IServiceCollection services, ServiceLifetime mediatorLifetime, params Assembly[] assemblies)
            => services.AddMediatorLite((IEnumerable<Assembly>)assemblies, mediatorLifetime);

        public static IServiceCollection AddMediatorLite(this IServiceCollection services, IEnumerable<Assembly> assemblies, ServiceLifetime mediatorLifetime = ServiceLifetime.Singleton)
        {
            if (!assemblies.Any())
                throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");

            ServiceRegistrar.AddMediatorLiteHandlers(services, assemblies);

            return services.TryAddMediatorLiteImplementation(mediatorLifetime);
        }

        public static IServiceCollection AddMediatorLiteHandlers(this IServiceCollection services, ServiceLifetime mediatorLifetime, params Type[] handlerAssemblyMarkerTypes)
            => services.AddMediatorLite((IEnumerable<Type>)handlerAssemblyMarkerTypes, mediatorLifetime);
        
        public static IServiceCollection AddMediatorLite(this IServiceCollection services, IEnumerable<Type> handlerAssemblyMarkerTypes, ServiceLifetime mediatorLifetime = ServiceLifetime.Singleton)
            => services.AddMediatorLite(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly), mediatorLifetime);
        
        public static IServiceCollection TryAddMediatorLiteImplementation(this IServiceCollection self, ServiceLifetime lifetime)
        {
            self.TryAdd(new ServiceDescriptor(typeof(DiServiceFactory), provider => new DiServiceFactory(provider), ServiceLifetime.Singleton));
            self.TryAdd(new ServiceDescriptor(typeof(IMediator), typeof(Mediator<DiServiceFactory>), lifetime));
            return self;
        }
    }
}