using Mediator.Lite.Abstraction;
using Mediator.Lite.Implementation;
using Mediator.Lite.Implementation.ServiceFactory;

namespace Mediator.Lite.Extension
{
    public static class ServiceFactoryExt
    {
        public static Mediator<TFactory> AsMediator<TFactory>(this TFactory self) where TFactory : IServiceFactory 
            => new Mediator<TFactory>(self);

        public static CacheServiceFactory<TFactory> AsCache<TFactory>(this TFactory self) where TFactory : IServiceFactory 
            => new CacheServiceFactory<TFactory>(self);
    }
}