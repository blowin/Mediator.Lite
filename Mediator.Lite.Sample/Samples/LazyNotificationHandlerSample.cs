using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Extension;

namespace Mediator.Lite.Sample.Samples
{
    [Description("Lazy NotificationHandler")]
    public class LazyNotificationHandlerSample : ISample
    {
        private class LazyMsg : INotification
        {
        
        }
    
        private class LazyHandler : INotificationHandler<LazyMsg>
        {
            public LazyHandler()
            {
                Console.WriteLine("LazyHandler - Ctor");
            }
        
            public ValueTask Handle(LazyMsg notification)
            {
                Console.WriteLine("LazyHandler - Handle");
                return ValueTaskUtil.Complete;
            }
        }
        
        public void Run()
        {
            var mediator = new MediatorBuilder()
                .AddNotificationHandlerAsLazy(() => new LazyHandler())
                .Builder();
            
            Console.WriteLine("Before publish");

            mediator.Publish(new LazyMsg());
            mediator.Publish(new LazyMsg());
            
            Console.WriteLine("After publish");
        }
    }
}