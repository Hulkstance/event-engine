using EventEngine.Shared;
using EventEngineMediatR;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

var provider = services.BuildServiceProvider();
var engine = new EventEngineMediatR.EventEngineMediatR(provider.GetRequiredService<IMediator>());

// Simulate some events
await engine.Put(new EventX(EventTypeConstants.EventTick, "Tick Data"));
await engine.Put(new EventX(EventTypeConstants.EventOrder, "Order Data"));

// Keep the program running
Console.ReadLine();

public class TimerEventHandler : INotificationHandler<EventX>
{
    public Task Handle(EventX e, CancellationToken cancellationToken)
    {
        if (e.Type == EventTypeConstants.EventTimer)
        {
            Console.WriteLine($"Timer Event: {e.Type}");
        }
        return Task.CompletedTask;
    }
}

public class GeneralEventHandler : INotificationHandler<EventX>
{
    public Task Handle(EventX e, CancellationToken cancellationToken)
    {
        Console.WriteLine($"General Event: {e.Type}, Data: {e.Data}");
        return Task.CompletedTask;
    }
}