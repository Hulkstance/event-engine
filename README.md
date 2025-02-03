# EventEngine

This is some random code I came up with while backporting a Python implementation into .NET. It’s a mix of event-driven design patterns and various concurrency models to explore different approaches for handling events.

### Summary of Libraries

| Library                     | Key Features                                                                 |
|-----------------------------|------------------------------------------------------------------------------|
| **System.Reactive (Rx.NET)** | Reactive programming, observable sequences, LINQ-style operators.            |
| **BackgroundService**        | Long-running tasks, integrated with .NET dependency injection.               |
| **MediatR**                  | Mediator pattern, supports commands, queries, and events.                    |
| **TPL Dataflow**             | Dataflow pipelines, message passing, and parallel processing.                |

Each library has its strengths and is suited for different scenarios. For example:
- Use **Rx.NET** for reactive programming.
- Use **BackgroundService** for long-running tasks in ASP.NET Core.
- Use **MediatR** for clean separation of concerns in CQRS/event-driven architectures.
- Use **TPL Dataflow** for building complex dataflow pipelines.
