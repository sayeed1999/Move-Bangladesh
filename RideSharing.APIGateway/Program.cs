var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"));

var app = builder.Build();

app.MapGet("/", () => "API Gateway is running.");
app.MapReverseProxy();

app.Run();
