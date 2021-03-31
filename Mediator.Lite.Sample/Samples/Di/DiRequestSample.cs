using System;
using System.ComponentModel;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Extension;
using Mediator.Lite.Extension.Microsoft.DependencyInjection;
using Mediator.Lite.Sample.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Mediator.Lite.Sample.Samples.Di
{
    [Description("DiRequestSample")]
    public class DiRequestSample : ISample
    {
        private class AppendHelloRequestHandler : IRequestHandler<LoginRequest, string>
        {
            public string Handle(LoginRequest request) => "******* Hello " + request.Name + " from AppendHelloRequestHandler *******";
        }

        public void Run()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRequestHandler<LoginRequest, string>, AppendHelloRequestHandler>()
                .TryAddMediatorLiteImplementation(ServiceLifetime.Singleton)
                .BuildServiceProvider();

            var mediator = serviceProvider.GetRequiredService<IMediator>();
            
            var loginRequest = new LoginRequest("Anna");

            var response = mediator.For<string>().Send(loginRequest);
            
            Console.WriteLine(response);
        }
    }
}