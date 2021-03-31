using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Extension
{
    public partial class MediatorBuilderExt
    {
        public static DictionaryServiceFactoryBuilder AddRequestHandler<TRequest, TResponse>(this DictionaryServiceFactoryBuilder self, IRequestHandler<TRequest, TResponse> handler) 
            where TRequest : IRequest<TResponse> =>
            self.AddRequestHandler(typeof(TRequest), handler);
    }
}