using System;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Implementation.Factory;
using Mediator.Lite.Implementation.RequestHandler;

namespace Mediator.Lite.Extension
{
    public partial class MediatorBuilderExt
    {
        public static MediatorBuilder AddRequestHandler<TRequest, TResponse>(this MediatorBuilder self, IRequestHandler<TRequest, TResponse> handler) 
            where TRequest : IRequest<TResponse> =>
            self.AddRequestHandler(typeof(TRequest), handler);

        public static MediatorBuilder AddRequestHandlerAsLazy<TRequest, TResponse>(this MediatorBuilder self,
            Func<IRequestHandler<TRequest, TResponse>> handlerFactory, bool isThreadSafe = true)
            where TRequest : IRequest<TResponse>
        {
            return self.AddRequestHandler(
                typeof(TRequest), 
                RequestHandlerWithFactory.Create<LazyFactory<IRequestHandler<TRequest, TResponse>>, TRequest, TResponse>(
                        new LazyFactory<IRequestHandler<TRequest, TResponse>>(
                                new Lazy<IRequestHandler<TRequest, TResponse>>(
                                        handlerFactory, 
                                        isThreadSafe
                                    )
                            )
                    )
                );
        }
    }
}