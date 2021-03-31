using System;
using System.Collections.Generic;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Implementation.ServiceFactory
{
    public readonly struct DictionaryServiceFactory : IServiceFactory
    {
        private readonly Dictionary<int, IRequestHandler> _requestHandlers;
        private readonly Dictionary<int, INotificationHandler[]> _notificationHandlers;

        public DictionaryServiceFactory(Dictionary<Type, IRequestHandler> requestHandler, Dictionary<Type, List<INotificationHandler>> notificationHandler)
        {
            _requestHandlers = new Dictionary<int, IRequestHandler>(requestHandler.Count);
            foreach (var keyValuePair in requestHandler)
                _requestHandlers.Add(keyValuePair.Key.GetHashCode(), keyValuePair.Value);
                
            _notificationHandlers = new Dictionary<int, INotificationHandler[]>(notificationHandler.Count);
            foreach (var keyValuePair in notificationHandler)
                _notificationHandlers.Add(keyValuePair.Key.GetHashCode(), keyValuePair.Value.ToArray());
        }
        
        public IEnumerable<INotificationHandler<TNotification>> GetNotificationHandlers<TNotification>() where TNotification : INotification
        {
            if (_notificationHandlers.TryGetValue(typeof(TNotification).GetHashCode(), out var handlers))
                return (INotificationHandler<TNotification>[])handlers;

            throw new InvalidOperationException("Not found handlers for " + typeof(TNotification).Name);
        }

        public IRequestHandler<TRequest, TResponse> GetRequestHandler<TRequest, TResponse>() 
            where TRequest : IRequest<TResponse>
        {
            if (_requestHandlers.TryGetValue(typeof(TRequest).GetHashCode(), out var handler))
                return (IRequestHandler<TRequest, TResponse>)handler;

            throw new InvalidOperationException("Not found handler for " + typeof(TRequest).Name);
        }
    }
}