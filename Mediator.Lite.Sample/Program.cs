using System;
using System.Threading.Tasks;
using Mediator.Lite.Abstraction;
using Mediator.Lite.Extension;

namespace Mediator.Lite.Sample
{
    class LoginRequest : IRequest<string>, INotification
    {
        public string Name { get; }

        public LoginRequest(string name)
        {
            Name = name;
        }
    }

    class LineRequestHandler : INotificationHandler<LoginRequest>
    {
        public ValueTask Handle(LoginRequest notification)
        {
            Console.WriteLine("_____________________________________________");
            return ValueTaskUtil.Complete;
        }
    }
    
    class AppendHelloRequestHandler : IRequestHandler<LoginRequest, string>
    {
        public string Handle(LoginRequest request) => "******* Hello " + request.Name + " from AppendHelloRequestHandler *******";
    }

    class PrintNotificationHandler : INotificationHandler<LoginRequest>
    {
        public ValueTask Handle(LoginRequest notification)
        {
            Console.WriteLine($"Hello {notification.Name}");
            return ValueTaskUtil.Complete;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var mediator = new MediatorBuilder()
                .AddRequestHandler(new AppendHelloRequestHandler())
                
                .AddNotificationHandler(new LineRequestHandler())
                .AddNotificationHandler(new PrintNotificationHandler())
                .AddNotificationHandler(new LineRequestHandler())
                
                .Builder();
            
            var loginRequest = new LoginRequest("Anna");

            mediator.Publish(loginRequest);
            
            mediator.Send(loginRequest, out string response);
            
            Console.WriteLine(response);
        }
    }
}