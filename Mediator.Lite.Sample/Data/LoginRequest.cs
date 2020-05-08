using Mediator.Lite.Abstraction;

namespace Mediator.Lite.Sample.Data
{
    public class LoginRequest : IRequest<string>, INotification
    {
        public string Name { get; }

        public LoginRequest(string name)
        {
            Name = name;
        }
    }
}