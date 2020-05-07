using System.Threading.Tasks;

namespace Mediator.Lite.Abstraction
{
    public interface INotificationHandler
    {
    }
    
    public interface INotificationHandler<in TNotification> : INotificationHandler
        where TNotification : INotification
    {
        ValueTask Handle(TNotification notification);
    }
}