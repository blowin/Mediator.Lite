using System.Threading;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Implementation
{
    public sealed class Mediator<TFactory> : IMediator
        where TFactory : IServiceFactory
    {
        private TFactory _factory;

        public Mediator(TFactory factory)
        {
            _factory = factory;
        }
            
        public async ValueTask Publish<TNotification>(TNotification notification) where TNotification : INotification
        {
            foreach (var notificationHandler in _factory.GetNotificationHandlers<TNotification>())
                await notificationHandler.Handle(notification);
        }

        public void Send<TRequest, TResponse>(TRequest request, out TResponse response) where TRequest : IRequest<TResponse>
        {
            var handler = _factory.GetRequestHandler<TRequest, TResponse>();
            response = handler.Handle(request);
        }

        public void SendAsync<TRequest, TResponse>(TRequest request, out ValueTask<TResponse> response, CancellationToken token = default) 
            where TRequest : IAsyncRequest<TResponse>
        {
            request.Token = token;
            Send(request, out response);
        }
    }
}