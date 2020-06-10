using Mediator.Lite.Abstraction;
using Mediator.Lite.Implementation;

namespace Mediator.Lite.Extension
{
    public static class ServiceFactoryExt
    {
        public static Mediator<TFactory> AsMediator<TFactory>(this TFactory self) where TFactory : IServiceFactory 
            => new Mediator<TFactory>(self);
    }
}