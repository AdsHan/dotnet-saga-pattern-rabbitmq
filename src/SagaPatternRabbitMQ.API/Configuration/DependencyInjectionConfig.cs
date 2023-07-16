using Microsoft.EntityFrameworkCore;
using SagaPatternRabbitMQ.API.Application.Services;
using SagaPatternRabbitMQ.API.Data;

namespace SagaPatternRabbitMQ.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services)
    {
        services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase("CatalogDB"));

        services.AddTransient<ProductPopulateService>();

        return services;
    }
}
