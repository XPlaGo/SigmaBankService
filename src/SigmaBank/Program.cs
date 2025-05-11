using SigmaBank;
using SigmaBank.Auth;
using SigmaBank.GrpcServices;
using SigmaBank.Infrastructure;
using SigmaBank.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddInfrastructure()
    .AddServices()
    .AddGrpcServices()
    .AddAuth(builder.Configuration);

WebApplication app = builder.Build();

app.MapGrpcServices();

app.Migrate();
app.Run();