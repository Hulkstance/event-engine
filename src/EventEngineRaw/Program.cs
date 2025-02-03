using EventEngine.Shared;

var engine = new EventEngineRaw.EventEngine();

// Register a handler for the timer event
engine.Register(EventTypeConstants.EventTimer, OnTimerEvent);

// Register a general handler for all events
engine.RegisterGeneral(OnAnyEvent);

// Start the engine
engine.Start();

// Simulate some events
engine.Put(new Event(EventTypeConstants.EventTick, "Tick Data"));
engine.Put(new Event(EventTypeConstants.EventOrder, "Order Data"));

// Keep the program running
Console.ReadLine();

// Stop the engine
engine.Stop();

void OnTimerEvent(Event e) => Console.WriteLine($"Timer Event: {e.Type}");

void OnAnyEvent(Event e) => Console.WriteLine($"General Event: {e.Type}, Data: {e.Data}");
