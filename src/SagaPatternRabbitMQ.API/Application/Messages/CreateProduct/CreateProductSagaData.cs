using Rebus.Sagas;

namespace SagaPatternRabbitMQ.API.Application.Messages.CreateProduct;

public class CreateProductSagaData : ISagaData
{
    public Guid Id { get; set; }
    public int Revision { get; set; }

    public Guid ProductId { get; set; }

    // Campos compartilhados
    public bool InvetoryChecked { get; set; }
    public bool CreatedIndex { get; set; }
    public bool EmailSent { get; set; }
}
