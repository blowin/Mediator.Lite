using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Extension
{
    public static class MediatorBuilderExt
    {
        public static MediatorBuilder AddRequestHandler<TRequest, TResponse>(this MediatorBuilder self, IRequestHandler<TRequest, TResponse> handler) 
            where TRequest : IRequest<TResponse> =>
            self.AddRequestHandler(typeof(TRequest), handler);

        public static MediatorBuilder AddNotificationHandler<TNotification>(this MediatorBuilder self, INotificationHandler<TNotification> handler) 
            where TNotification : INotification =>
            self.AddNotificationHandler(typeof(TNotification), handler);
    }
}