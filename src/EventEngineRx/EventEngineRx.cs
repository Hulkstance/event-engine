using System.Reactive.Linq;
using System.Reactive.Subjects;
using EventEngine.Shared;

namespace EventEngineRx;

public class EventEngineRx
{
    private readonly Subject<Event> _eventStream = new();
    private readonly IDisposable _timerSubscription;

    public IObservable<Event> EventStream => _eventStream.AsObservable();

    public EventEngineRx(int interval = 1)
    {
        // Start the timer
        _timerSubscription = Observable.Interval(TimeSpan.FromSeconds(interval))
            .Subscribe(_ => _eventStream.OnNext(new Event(EventTypeConstants.EventTimer)));
    }

    public void Put(Event e)
    {
        _eventStream.OnNext(e);
    }

    public void Stop()
    {
        _timerSubscription.Dispose();
        _eventStream.OnCompleted();
    }
}
