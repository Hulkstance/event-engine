namespace EventEngine.Shared;

public sealed record Event(string Type, object? Data = null);
