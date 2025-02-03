using MediatR;

namespace EventEngineMediatR;

public sealed record EventX(string Type, object? Data = null) : INotification;

public class EventEngineMediatR
{
    private readonly IMediator _mediator;

    public EventEngineMediatR(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Put(EventX e)
    {
        await _mediator.Publish(e);
    }
}
