using EventEngine.Shared;

var engine = new EventEngineDataflow.EventEngineDataflow();

// Simulate some events
engine.Put(new Event(EventTypeConstants.EventTick, "Tick Data"));
engine.Put(new Event(EventTypeConstants.EventOrder, "Order Data"));

// Keep the program running
Console.ReadLine();

// Stop the engine
engine.Stop();
