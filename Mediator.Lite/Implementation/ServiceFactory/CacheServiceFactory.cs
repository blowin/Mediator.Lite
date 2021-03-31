using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Implementation.ServiceFactory
{
    public sealed class CacheServiceFactory<TFactory> : IServiceFactory
        where TFactory : IServiceFactory
    {
        private readonly TFactory _factory;
        private readonly ConcurrentDictionary<int, INotificationHandler[]> _notificationHandlers;
        private readonly ConcurrentDictionary<int, IRequestHandler> _requestHandlers;

        public CacheServiceFactory(TFactory factory)
        {
            _factory = factory;
            _notificationHandlers = new ConcurrentDictionary<int, INotificationHandler[]>();
            _requestHandlers = new ConcurrentDictionary<int, IRequestHandler>();
        }
        
        public IEnumerable<INotificationHandler<TNotification>> GetNotificationHandlers<TNotification>() where TNotification : INotification
        {
            var res = _notificationHandlers.GetOrAdd(
                typeof(TNotification).GetHashCode(), 
                _ => _factory.GetNotificationHandlers<TNotification>().ToArray());
            
            return (IEnumerable<INotificationHandler<TNotification>>) res;
        }

        public IRequestHandler<TRequest, TResponse> GetRequestHandler<TRequest, TResponse>() where TRequest : IRequest<TResponse>
        {
            var res = _requestHandlers.GetOrAdd(
                typeof(TRequest).GetHashCode(),
                _ => _factory.GetRequestHandler<TRequest, TResponse>());

            return (IRequestHandler<TRequest, TResponse>) res;
        }
    }
}