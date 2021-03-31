using System.Threading;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;

namespace Mediator.Lite
{
    public readonly struct ConcreteResponseMediator<TResponse>
    {
        private readonly IMediator _mediator;

        public ConcreteResponseMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public ValueTask Publish<TNotification>(TNotification notification)
            where TNotification : INotification
        {
            return _mediator.Publish(notification);
        }
        
        public TResponse Send<TRequest>(TRequest request) 
            where TRequest : IRequest<TResponse>
        {
            _mediator.Send<TRequest, TResponse>(request, out var res);
            return res;
        }

        public ValueTask<TResponse> SendAsync<TRequest>(TRequest request, CancellationToken token = default) 
            where TRequest : IAsyncRequest<TResponse>
        {
            _mediator.SendAsync<TRequest, TResponse>(request, out var res, token);
            return res;
        }
    }
}