using Rebus.Bus;
using Rebus.Handlers;

namespace SagaPatternRabbitMQ.API.Application.Messages.CreateProduct.Commands;

internal class CreateIndexSearchHandler : IHandleMessages<CreateIndexSearchCommand>
{
    private readonly ILogger<CreateIndexSearchHandler> _logger;
    private readonly IBus _bus;

    public CreateIndexSearchHandler(
        ILogger<CreateIndexSearchHandler> logger,
        IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(CreateIndexSearchCommand message)
    {
        _logger.LogInformation($"Produto {message.ProductId}");

        await Task.Delay(3000);

        await _bus.Send(new ProductCreatedIndexSearchEvent(message.ProductId));
    }
}