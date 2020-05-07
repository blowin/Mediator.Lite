using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite
{
    public sealed class MediatorBuilder
    {
        private readonly Dictionary<Type, IRequestHandler> _requestHand;
        private readonly Dictionary<Type, List<INotificationHandler>> _notificationHandlers;

        public MediatorBuilder()
        {
            _requestHand = new Dictionary<Type, IRequestHandler>();
            _notificationHandlers = new Dictionary<Type, List<INotificationHandler>>();
        }
        
        public MediatorBuilder AddRequestHandler(Type type, IRequestHandler handler)
        {
            _requestHand.Add(type, handler);
            return this;
        }
        
        public MediatorBuilder AddNotificationHandler(Type type, INotificationHandler handler)
        {
            if (_notificationHandlers.TryGetValue(type, out var store))
            {
                store.Add(handler);
                return this;
            }

            store = new List<INotificationHandler>
            {
                handler
            };
            
            _notificationHandlers.Add(type, store);
            return this;
        }

        public IMediator Builder()
        {
            return new Mediator(_requestHand, _notificationHandlers);
        }
        
        private sealed class Mediator : IMediator
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
}