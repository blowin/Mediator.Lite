namespace Mediator.Lite.Abstraction
{
    public interface IRequest<out TResponse>
    {
        
    }

    public interface IRequest : IRequest<Void>
    {
    }
}