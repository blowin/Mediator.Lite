using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Extension
{
    public static class MediatorExt
    {
        /// <summary>
        /// Принимает запросы которые возвращают конкретный ответ <see cref="TResponse"/>
        /// </summary>
        public static ConcreteResponseMediator<TResponse> For<TResponse>(this IMediator self)
            => new ConcreteResponseMediator<TResponse>(self);
    }
}