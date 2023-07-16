using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using SagaPatternRabbitMQ.API.Application.InputModel;
using SagaPatternRabbitMQ.API.Application.Messages.CreateProduct;

namespace SagaPatternRabbitMQ.API.Data.Repositories;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IBus _bus;

    public ProductsController(IBus bus)
    {
        _bus = bus;
    }

    // POST api/products/
    /// <summary>
    /// Grava o produto
    /// </summary>   
    /// <remarks>
    /// Exemplo request:
    ///
    ///     POST / Produto
    ///     {
    ///         "title": "Sandalia",
    ///         "description": "Sandália Preta Couro Salto Fino",
    ///         "price": 249.50,
    ///         "quantity": 100       
    ///     }
    /// </remarks>        
    /// <returns>Retorna objeto criado da classe Produto</returns>                
    /// <response code="201">O produto foi incluído corretamente</response>                
    /// <response code="400">Falha na requisição</response>         
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ActionName("NewProduct")]
    public async Task<IActionResult> PostAsync([FromBody] CreateProductInputModel input)
    {
        // Rotina de inclusão do produto que vai gerar um Id de produto
        var id = Guid.NewGuid();

        await _bus.Send(new ProductCreatedEvent(id));

        return Accepted();
    }

}
