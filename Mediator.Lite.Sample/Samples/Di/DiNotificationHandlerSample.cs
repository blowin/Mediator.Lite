using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Extension;
using Mediator.Lite.Extension.Microsoft.DependencyInjection;
using Mediator.Lite.Sample.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Lite.Sample.Samples.Di
{
    [Description("DiNotificationHandlerSample")]
    public class DiNotificationHandlerSample : ISample
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
            var serviceProvider = new ServiceCollection()
                .AddSingleton<INotificationHandler<LoginRequest>, LineRequestHandler>()
                .AddSingleton<INotificationHandler<LoginRequest>, PrintNotificationHandler>()
                .AddSingleton<INotificationHandler<LoginRequest>, LineRequestHandler>()
                .TryAddMediatorLiteImplementation(ServiceLifetime.Singleton)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            
            var loginRequest = new LoginRequest("Anna");

            mediator.Publish(loginRequest);
        }
    }
}