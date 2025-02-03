using System.Threading.Tasks.Dataflow;
using EventEngine.Shared;

namespace EventEngineDataflow;

public class EventEngineDataflow
{
    private readonly ActionBlock<Event> _eventBlock;

    public EventEngineDataflow() =>
        _eventBlock = new ActionBlock<Event>(e =>
        {
            Console.WriteLine($"Event: {e.Type}, Data: {e.Data}");
        });

    public void Put(Event e) => _eventBlock.Post(e);

    public void Stop()
    {
        _eventBlock.Complete();
        _eventBlock.Completion.Wait();
    }
}
