using System.Threading.Tasks;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Implementation.NotificationHandler
{
    internal sealed class NotificationHandlerWithFactory<TFactory, TNotification> : INotificationHandler<TNotification> 
        where TNotification : INotification
        where TFactory : IFactory<INotificationHandler<TNotification>>
    {
        private TFactory _factory;

        public NotificationHandlerWithFactory(TFactory factory)
        {
            _factory = factory;
        }

        public ValueTask Handle(TNotification notification) => _factory.Create().Handle(notification);
    }

    public static class NotificationHandlerWithFactory
    {
        public static INotificationHandler<TNotification> Create<TFactory, TNotification>(TFactory factory) 
            where TNotification : INotification 
            where TFactory : IFactory<INotificationHandler<TNotification>>
        {
            return new NotificationHandlerWithFactory<TFactory, TNotification>(factory);
        }
    }
}