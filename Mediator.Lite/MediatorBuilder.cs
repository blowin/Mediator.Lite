using System;
using System.Collections.Generic;
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
            return new Implementation.Mediator(_requestHand, _notificationHandlers);
        }
    }
}