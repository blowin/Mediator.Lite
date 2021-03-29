using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Extension;
using Mediator.Lite.Sample.Data;

namespace Mediator.Lite.Sample.Samples
{
    [Description("NotificationHandler")]
    public class NotificationHandlerSample : ISample
    {
        private class LineRequestHandler : INotificationHandler<LoginRequest>
        {
            public ValueTask Handle(LoginRequest notification)
            {
                Console.WriteLine("_____________________________________________");
                return ValueTaskUtil.Complete;
            }
        }

        private class PrintNotificationHandler : INotificationHandler<LoginRequest>
        {
            public ValueTask Handle(LoginRequest notification)
            {
                Console.WriteLine($"Hello {notification.Name}");
                return ValueTaskUtil.Complete;
            }
        }

        public void Run()
        {
            var mediator = new DictionaryServiceFactoryBuilder()
                .AddNotificationHandler(new LineRequestHandler())
                .AddNotificationHandler(new PrintNotificationHandler())
                .AddNotificationHandler(new LineRequestHandler())
                
                .Build()
                .AsMediator();
            
            var loginRequest = new LoginRequest("Anna");

            mediator.Publish(loginRequest);
        }
    }
}