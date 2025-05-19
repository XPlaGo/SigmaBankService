namespace SigmaBank.GrpcServices;

internal static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGrpcServices(this IEndpointRouteBuilder builder)
    {
        builder.MapGrpcService<AuthGrpcService>();
        builder.MapGrpcService<AccountsGrpcService>();

        builder.MapGrpcReflectionService();

        return builder;
    }
}