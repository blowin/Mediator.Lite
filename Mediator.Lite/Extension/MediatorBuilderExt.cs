using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Extension
{
    public static partial class MediatorBuilderExt
    {
        public static DictionaryServiceFactoryBuilder AddRequestHandler<TRequest, TResponse>(this DictionaryServiceFactoryBuilder self, IRequestHandler<TRequest, TResponse> handler) 
            where TRequest : IRequest<TResponse> =>
            self.AddRequestHandler(typeof(TRequest), handler);
        
        public static DictionaryServiceFactoryBuilder AddNotificationHandler<TNotification>(this DictionaryServiceFactoryBuilder self, INotificationHandler<TNotification> handler) 
            where TNotification : INotification =>
            self.AddNotificationHandler(typeof(TNotification), handler);
    }
}