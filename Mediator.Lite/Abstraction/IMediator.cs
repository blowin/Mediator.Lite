using System.Threading.Tasks;

namespace Mediator.Lite.Abstraction
{
    public interface IMediator
    {
        ValueTask Publish<TNotification>(TNotification notification) 
            where TNotification : INotification;
        
        void Send<TRequest, TResponse>(TRequest request, out TResponse response) 
            where TRequest : IRequest<TResponse>;
    }
}