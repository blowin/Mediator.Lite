# Mediator.Lite

Request and notification class:

```c#
public class LoginRequest : IRequest<string>, INotification
{
    public string Name { get; }

    public LoginRequest(string name)
    {
        Name = name;
    }
}
```



Request handler

```c#
private class AppendHelloRequestHandler : IRequestHandler<LoginRequest, string>
{
public string Handle(LoginRequest request) => "******* Hello " + request.Name + " from AppendHelloRequestHandler *******";
}
```



Usage

```c#
var mediator = new DictionaryServiceFactoryBuilder()
                .AddRequestHandler(new AppendHelloRequestHandler())
                .Build()
                .AsMediator();

var loginRequest = new LoginRequest("Anna");

mediator.Send(loginRequest, out string response);

Console.WriteLine(response);
```

With For shortcut

```c#
var mediator = new DictionaryServiceFactoryBuilder()
                .AddRequestHandler(new AppendHelloRequestHandler())
                .Build()
                .AsMediator();
            
var loginRequest = new LoginRequest("Anna");

var response = mediator.For<string>().Send(loginRequest);

Console.WriteLine(response);
```



Notification handler

```c#
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

// usage

var mediator = new DictionaryServiceFactoryBuilder()
                .AddNotificationHandler(new LineRequestHandler())
                .AddNotificationHandler(new PrintNotificationHandler())
                .AddNotificationHandler(new LineRequestHandler())
                
                .Build()
                .AsMediator();
            
var loginRequest = new LoginRequest("Anna");

mediator.Publish(loginRequest);
/*
Output
_____________________________________________
Hello Anna
_____________________________________________
*/
```



Support Microsoft Dependency injection

```c#
var serviceProvider = new ServiceCollection()
                .AddSingleton<IRequestHandler<LoginRequest, string>, AppendHelloRequestHandler>()
                .TryAddMediatorLiteImplementation(ServiceLifetime.Singleton)
                .BuildServiceProvider();

var mediator = serviceProvider.GetRequiredService<IMediator>();

var loginRequest = new LoginRequest("Anna");

var response = mediator.For<string>().Send(loginRequest);

Console.WriteLine(response);
```

