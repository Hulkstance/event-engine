using EventEngine.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<EventEngineBackgroundService.EventEngineBackgroundService>();
        services.AddHostedService(provider => provider.GetRequiredService<EventEngineBackgroundService.EventEngineBackgroundService>());
    })
    .Build();

// NOTE: StartAsync is used instead of RunAsync so it doesn't block execution, making it easier to run the code below.
await host.StartAsync();

var engine = host.Services.GetRequiredService<EventEngineBackgroundService.EventEngineBackgroundService>();

// Simulate some events
engine.Put(new Event(EventTypeConstants.EventTick, "Tick Data"));
engine.Put(new Event(EventTypeConstants.EventOrder, "Order Data"));

Console.ReadLine();

await host.StopAsync();
