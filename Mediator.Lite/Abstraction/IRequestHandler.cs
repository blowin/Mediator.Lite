using System.Threading.Tasks;

namespace Mediator.Lite.Abstraction
{
    public interface IRequestHandler
    {
    }
    
    public interface IRequestHandler<in TRequest, out TResponse> : IRequestHandler
        where TRequest : IRequest<TResponse>
    {
        TResponse Handle(TRequest request);
    }

    public interface IRequestHandler<in TRequest> : IRequestHandler<TRequest, Void>
        where TRequest : IRequest<Void>
    {
    }
    
    public interface IAsyncRequestHandler<in TRequest, TResponse> : IRequestHandler
        where TRequest : IAsyncRequest<TResponse>
    {
        ValueTask<TResponse> Handle(TRequest request);
    }

    public interface IAsyncRequestHandler<in TRequest> : IAsyncRequestHandler<TRequest, Void>
        where TRequest : IAsyncRequest<Void>
    {
    }
}