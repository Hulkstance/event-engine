using System.Reactive.Linq;
using EventEngine.Shared;

var engine = new EventEngineRx.EventEngineRx();

// Subscribe to specific events
engine.EventStream
    .Where(e => e.Type == EventTypeConstants.EventTimer)
    .Subscribe(e => Console.WriteLine($"Timer Event: {e.Type}"));

// Subscribe to all events
engine.EventStream.Subscribe(e => Console.WriteLine($"General Event: {e.Type}, Data: {e.Data}"));

// Simulate some events
engine.Put(new Event(EventTypeConstants.EventTick, "Tick Data"));
engine.Put(new Event(EventTypeConstants.EventOrder, "Order Data"));

// Keep the program running
Console.ReadLine();

// Stop the engine
engine.Stop();
