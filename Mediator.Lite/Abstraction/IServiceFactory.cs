using System.Collections.Generic;

namespace Mediator.Lite.Abstraction
{
    public interface IServiceFactory
    {
        IEnumerable<INotificationHandler<TNotification>> GetNotificationHandlers<TNotification>() 
            where TNotification : INotification;
        
        IRequestHandler<TRequest, TResponse> GetRequestHandler<TRequest, TResponse>() 
            where TRequest : IRequest<TResponse>;
    }
}