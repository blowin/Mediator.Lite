using System;
using System.Collections.Generic;
using Mediator.Lite.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Lite.Extension.Microsoft.DependencyInjection
{
    public sealed class DiServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _provider;

        public DiServiceFactory(IServiceProvider provider)
        {
            _provider = provider;
        }
        public IEnumerable<INotificationHandler<TNotification>> GetNotificationHandlers<TNotification>() where TNotification : INotification 
            => _provider.GetServices<INotificationHandler<TNotification>>();

        public IRequestHandler<TRequest, TResponse> GetRequestHandler<TRequest, TResponse>()
            where TRequest : IRequest<TResponse>
            => _provider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
    }
}