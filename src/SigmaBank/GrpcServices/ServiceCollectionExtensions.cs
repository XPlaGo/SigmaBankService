namespace SigmaBank.GrpcServices;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcServices(this IServiceCollection services)
    {
        services.AddGrpc();
        services.AddGrpcReflection();

        return services;
    }
}