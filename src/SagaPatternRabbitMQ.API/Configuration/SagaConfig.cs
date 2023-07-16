using Rebus.Config;
using Rebus.Routing.TypeBased;
using SagaPatternRabbitMQ.API.Application.Messages.CreateProduct;

namespace SagaPatternRabbitMQ.API.Configuration;

public static class SagaConfig
{
    public static IServiceCollection AddsagaConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRebus(
            configure => configure
                .Logging(l => l.Console())
                .Routing(r => r.TypeBased().MapAssemblyOf<ProductCreatedEvent>("saga-pattern"))
                .Transport(t => t.UseRabbitMq(configuration.GetConnectionString("RabbitMQCs"), "saga-pattern"))
                .Sagas(s => s.StoreInPostgres(configuration.GetConnectionString("PostgreSQLCs"), "sagas", "indexes")),
            onCreated: async bus =>
            {
                Console.WriteLine("Barramento Rebus criado com sucesso!");
            });

        services.AutoRegisterHandlersFromAssemblyOf<ProductCreatedEvent>();

        return services;
    }
}

