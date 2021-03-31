using System.Threading;
using System.Threading.Tasks;

namespace Mediator.Lite.Abstraction
{
    public interface IRequest<out TResponse>
    {
        
    }

    public interface IAsyncRequest<TResponse> : IRequest<ValueTask<TResponse>>
    {
        CancellationToken Token { get; set; }
    }

    public interface IAsyncRequest : IAsyncRequest<Void>
    {
    }
    
    public interface IRequest : IRequest<Void>
    {
    }
}