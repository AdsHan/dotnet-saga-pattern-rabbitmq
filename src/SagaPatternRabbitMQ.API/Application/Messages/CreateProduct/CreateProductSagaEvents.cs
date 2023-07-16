namespace SagaPatternRabbitMQ.API.Application.Messages.CreateProduct;

// Steps ou Eventos da Saga (no passado)
public record ProductCreatedEvent(Guid ProductId);
public record ProductCheckedInventoryEvent(Guid ProductId);
public record ProductInclusionEmailSentEvent(Guid ProductId);
public record ProductCreatedIndexSearchEvent(Guid ProductId);
public record ProductCompletedEvent(Guid ProductId);

// Comandos (Handlers). Poderia ser Handlers do MediatR
public record CheckIventoryCommand(Guid ProductId);
public record CreateIndexSearchCommand(Guid ProductId);
public record SendInclusionEmailCommand(Guid ProductId);
