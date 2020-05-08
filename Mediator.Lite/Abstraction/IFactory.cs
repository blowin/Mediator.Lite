namespace Mediator.Lite.Abstraction
{
    public interface IFactory<out T>
    {
        T Create();
    }
}