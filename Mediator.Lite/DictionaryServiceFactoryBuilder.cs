using System;
using System.Collections.Generic;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Implementation.ServiceFactory;

namespace Mediator.Lite
{
    public sealed class DictionaryServiceFactoryBuilder
    {
        private readonly Dictionary<Type, IRequestHandler> _requestHand;
        private readonly Dictionary<Type, List<INotificationHandler>> _notificationHandlers;

        public DictionaryServiceFactoryBuilder()
        {
            _requestHand = new Dictionary<Type, IRequestHandler>();
            _notificationHandlers = new Dictionary<Type, List<INotificationHandler>>();
        }
        
        public DictionaryServiceFactoryBuilder AddRequestHandler(Type type, IRequestHandler handler)
        {
            _requestHand.Add(type, handler);
            return this;
        }
        
        public DictionaryServiceFactoryBuilder AddNotificationHandler(Type type, INotificationHandler handler)
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

        public DictionaryServiceFactory Build()
        {
            return new DictionaryServiceFactory(_requestHand, _notificationHandlers);
        }
    }
}