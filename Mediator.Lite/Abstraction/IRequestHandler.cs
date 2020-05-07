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
}