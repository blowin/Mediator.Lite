using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Extension
{
    public partial class MediatorBuilderExt
    {
        public static DictionaryServiceFactoryBuilder AddNotificationHandler<TNotification>(this DictionaryServiceFactoryBuilder self, INotificationHandler<TNotification> handler) 
            where TNotification : INotification =>
            self.AddNotificationHandler(typeof(TNotification), handler);
    }
}