using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace SagaPatternRabbitMQ.API.Application.Messages.CreateProduct;

public class CreateProductSaga : Saga<CreateProductSagaData>,
    IAmInitiatedBy<ProductCreatedEvent>,
    IHandleMessages<ProductCheckedInventoryEvent>,
    IHandleMessages<ProductCreatedIndexSearchEvent>,
    IHandleMessages<ProductInclusionEmailSentEvent>,
    IHandleMessages<ProductCompletedEvent>
{
    private readonly IBus _bus;

    public CreateProductSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<CreateProductSagaData> config)
    {
        config.Correlate<ProductCreatedEvent>(m => m.ProductId, s => s.ProductId);
        config.Correlate<ProductCheckedInventoryEvent>(m => m.ProductId, s => s.ProductId);
        config.Correlate<ProductCreatedIndexSearchEvent>(m => m.ProductId, s => s.ProductId);
        config.Correlate<ProductInclusionEmailSentEvent>(m => m.ProductId, s => s.ProductId);
        config.Correlate<ProductCompletedEvent>(m => m.ProductId, s => s.ProductId);
    }

    public async Task Handle(ProductCreatedEvent message)
    {
        if (!IsNew) return;

        await _bus.Send(new CheckIventoryCommand(message.ProductId));
    }

    public async Task Handle(ProductCheckedInventoryEvent message)
    {
        Data.InvetoryChecked = true;

        await _bus.Send(new CreateIndexSearchCommand(message.ProductId));
    }

    public async Task Handle(ProductCreatedIndexSearchEvent message)
    {
        Data.CreatedIndex = true;

        await _bus.Send(new SendInclusionEmailCommand(message.ProductId));
    }

    public async Task Handle(ProductInclusionEmailSentEvent message)
    {
        Data.EmailSent = true;

        await _bus.SendLocal(new ProductCompletedEvent(message.ProductId));
    }

    public Task Handle(ProductCompletedEvent message)
    {
        MarkAsComplete();

        return Task.CompletedTask;
    }
}
