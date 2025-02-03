using System.Threading.Channels;
using EventEngine.Shared;
using Microsoft.Extensions.Hosting;

namespace EventEngineBackgroundService;

public class EventEngineBackgroundService : BackgroundService
{
    private readonly Channel<Event> _eventChannel = Channel.CreateUnbounded<Event>();

    public void Put(Event e) => _eventChannel.Writer.TryWrite(e);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_eventChannel.Reader.TryRead(out var e))
            {
                Console.WriteLine($"Event: {e.Type}, Data: {e.Data}");
            }
            await Task.Delay(100, stoppingToken); // Avoid busy-waiting
        }
    }
}
