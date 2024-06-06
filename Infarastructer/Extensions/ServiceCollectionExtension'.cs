using Microsoft.Extensions.DependencyInjection;

namespace Infarastructer.Extensions;

public static class ServiceCollectionExtension_
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddService();
    }
    public static void AddService(this IServiceCollection services)
    {

    }
}
