using System.Collections.Concurrent;
using EventEngine.Shared;

namespace EventEngineRaw;

public class EventEngine
{
    // Handler delegate
    public delegate void EventHandler(Event e);

    private readonly ConcurrentQueue<Event> _queue = new();
    private readonly Dictionary<string, List<EventHandler>> _handlers = new();
    private readonly List<EventHandler> _generalHandlers = [];
    private readonly int _interval;
    private bool _active;
    private Thread? _eventThread;
    private Thread? _timerThread;

    public EventEngine(int interval = 1)
    {
        _interval = interval;
    }

    public void Start()
    {
        _active = true;

        // Start the event processing thread
        _eventThread = new Thread(Run);
        _eventThread.Start();

        // Start the timer thread
        _timerThread = new Thread(RunTimer);
        _timerThread.Start();
    }

    public void Stop()
    {
        _active = false;

        // Wait for threads to finish
        _timerThread?.Join();
        _eventThread?.Join();
    }

    public void Put(Event e) => _queue.Enqueue(e);

    public void Register(string type, EventHandler handler)
    {
        if (!_handlers.ContainsKey(type))
        {
            _handlers[type] = new List<EventHandler>();
        }

        if (!_handlers[type].Contains(handler))
        {
            _handlers[type].Add(handler);
        }
    }

    public void Unregister(string type, EventHandler handler)
    {
        if (_handlers.ContainsKey(type))
        {
            _handlers[type].Remove(handler);

            if (_handlers[type].Count == 0)
            {
                _handlers.Remove(type);
            }
        }
    }

    public void RegisterGeneral(EventHandler handler)
    {
        if (!_generalHandlers.Contains(handler))
        {
            _generalHandlers.Add(handler);
        }
    }

    public void UnregisterGeneral(EventHandler handler) => _generalHandlers.Remove(handler);

    private void Run()
    {
        while (_active)
        {
            if (_queue.TryDequeue(out var e))
            {
                Process(e);
            }
            else
            {
                Thread.Sleep(100); // Sleep for a short time to avoid busy-waiting
            }
        }
    }

    private void Process(Event e)
    {
        try
        {
            // Distribute to specific handlers
            if (_handlers.ContainsKey(e.Type))
            {
                foreach (var handler in _handlers[e.Type])
                {
                    handler(e);
                }
            }

            // Distribute to general handlers
            foreach (var handler in _generalHandlers)
            {
                handler(e);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing event: {ex}");
        }
    }

    private void RunTimer()
    {
        while (_active)
        {
            Thread.Sleep(_interval * 1000); // Sleep for the specified interval
            Put(new Event(EventTypeConstants.EventTimer));
        }
    }
}
