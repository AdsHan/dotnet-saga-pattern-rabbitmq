using Rebus.Bus;
using Rebus.Handlers;

namespace SagaPatternRabbitMQ.API.Application.Messages.CreateProduct.Commands;

internal class SendInclusionEmailHandler : IHandleMessages<SendInclusionEmailCommand>
{
    private readonly ILogger<SendInclusionEmailHandler> _logger;
    private readonly IBus _bus;

    public SendInclusionEmailHandler(
        ILogger<SendInclusionEmailHandler> logger,
        IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(SendInclusionEmailCommand message)
    {
        _logger.LogInformation($"Produto {message.ProductId}");

        await Task.Delay(3000);

        await _bus.Send(new ProductInclusionEmailSentEvent(message.ProductId));
    }
}