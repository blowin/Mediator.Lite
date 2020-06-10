using System;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Implementation.Factory;
using Mediator.Lite.Implementation.NotificationHandler;

namespace Mediator.Lite.Extension
{
    public partial class MediatorBuilderExt
    {
        public static DictionaryServiceFactoryBuilder AddNotificationHandler<TNotification>(this DictionaryServiceFactoryBuilder self, INotificationHandler<TNotification> handler) 
            where TNotification : INotification =>
            self.AddNotificationHandler(typeof(TNotification), handler);

        public static DictionaryServiceFactoryBuilder AddNotificationHandlerAsLazy<TNotification>(this DictionaryServiceFactoryBuilder self,
            Func<INotificationHandler<TNotification>> handlerFactory, bool isThreadSafe = true)
            where TNotification : INotification
        {
            return self.AddNotificationHandler(
                typeof(TNotification), 
                NotificationHandlerWithFactory.Create<LazyFactory<INotificationHandler<TNotification>>, TNotification>(
                    new LazyFactory<INotificationHandler<TNotification>>(
                        new Lazy<INotificationHandler<TNotification>>(
                            handlerFactory, 
                            isThreadSafe
                        )
                    )
                )
            );
        }
    }
}