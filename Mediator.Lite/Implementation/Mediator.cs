using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Implementation
{
    internal sealed class Mediator : IMediator
    {
        private readonly Dictionary<int, IRequestHandler> _requestHandlers;
        private readonly Dictionary<int, INotificationHandler[]> _notificationHandlers;

        public Mediator(Dictionary<Type, IRequestHandler> requestHandler, Dictionary<Type, List<INotificationHandler>> notificationHandler)
        {
            _requestHandlers = new Dictionary<int, IRequestHandler>(requestHandler.Count);
            foreach (var keyValuePair in requestHandler)
                _requestHandlers.Add(keyValuePair.Key.GetHashCode(), keyValuePair.Value);
                
            _notificationHandlers = new Dictionary<int, INotificationHandler[]>(notificationHandler.Count);
            foreach (var keyValuePair in notificationHandler)
                _notificationHandlers.Add(keyValuePair.Key.GetHashCode(), keyValuePair.Value.ToArray());
        }
            
        public async ValueTask Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            if (_notificationHandlers.TryGetValue(typeof(TNotification).GetHashCode(), out var handlers))
            {
                foreach (INotificationHandler<TNotification> notificationHandler in handlers)
                    await notificationHandler.Handle(notification);

                return;
            }
                
            throw new InvalidOperationException("Not found handlers for " + typeof(TNotification).Name);
        }

        public void Send<TRequest, TResponse>(TRequest request, out TResponse response) where TRequest : IRequest<TResponse>
        {
            if (_requestHandlers.TryGetValue(typeof(TRequest).GetHashCode(), out var handlers))
            {
                response = ((IRequestHandler<TRequest, TResponse>) handlers).Handle(request);
                return;
            }

            response = default;
            throw new InvalidOperationException("Not found handler for " + typeof(TRequest).Name);
        }
    }
}