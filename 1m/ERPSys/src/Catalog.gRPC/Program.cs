using Catalog.gRPC.Extentions;
using Catalog.gRPC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.AddApplictionaServices();
// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

var app = builder.Build();

app.MapGrpcReflectionService();

// Configure the HTTP request pipeline.
app.MapGrpcService<CatalogService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();