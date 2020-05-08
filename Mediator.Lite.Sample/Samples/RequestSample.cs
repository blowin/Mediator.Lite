using System;
using System.ComponentModel;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Extension;
using Mediator.Lite.Sample.Data;

namespace Mediator.Lite.Sample.Samples
{
    [Description("RequestSample")]
    public class RequestSample : ISample
    {
        private class AppendHelloRequestHandler : IRequestHandler<LoginRequest, string>
        {
            public string Handle(LoginRequest request) => "******* Hello " + request.Name + " from AppendHelloRequestHandler *******";
        }

        public void Run()
        {
            var mediator = new MediatorBuilder()
                .AddRequestHandler(new AppendHelloRequestHandler())
                .Builder();
            
            var loginRequest = new LoginRequest("Anna");

            mediator.Send(loginRequest, out string response);
            
            Console.WriteLine(response);
        }
    }
}