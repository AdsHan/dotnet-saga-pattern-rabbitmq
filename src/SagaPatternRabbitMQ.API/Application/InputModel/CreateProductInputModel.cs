namespace SagaPatternRabbitMQ.API.Application.InputModel;

public sealed record CreateProductInputModel(string Title, string Description, double Price, int Quantity);
