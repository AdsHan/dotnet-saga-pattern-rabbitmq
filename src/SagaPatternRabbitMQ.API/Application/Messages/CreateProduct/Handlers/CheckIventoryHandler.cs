using Rebus.Bus;
using Rebus.Handlers;

namespace SagaPatternRabbitMQ.API.Application.Messages.CreateProduct.Commands;

internal class CheckIventoryHandler : IHandleMessages<CheckIventoryCommand>
{
    private readonly ILogger<CheckIventoryHandler> _logger;
    private readonly IBus _bus;

    public CheckIventoryHandler(
        ILogger<CheckIventoryHandler> logger,
        IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(CheckIventoryCommand message)
    {
        _logger.LogInformation($"Produto {message.ProductId}");

        await Task.Delay(3000);

        await _bus.Send(new ProductCheckedInventoryEvent(message.ProductId));
    }
}