using System;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Implementation.NotificationHandler;

namespace Mediator.Lite.Implementation.RequestHandler
{
    internal sealed class RequestHandlerWithFactory<TFactory, TRequest, TResponse> : IRequestHandler<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>
        where TFactory : IFactory<IRequestHandler<TRequest, TResponse>>
    {
        private TFactory _factory;

        public RequestHandlerWithFactory(TFactory factory)
        {
            _factory = factory;
        }
        
        public TResponse Handle(TRequest request) => _factory.Create().Handle(request);
    }
    
    public static class RequestHandlerWithFactory
    {
        public static IRequestHandler<TRequest, TResponse>  Create<TFactory, TRequest, TResponse>(TFactory factory) 
            where TRequest : IRequest<TResponse> 
            where TFactory : IFactory<IRequestHandler<TRequest, TResponse>>
        {
            return new RequestHandlerWithFactory<TFactory, TRequest, TResponse>(factory);
        }
    }
}