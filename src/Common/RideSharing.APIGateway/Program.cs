using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("yarp"))
    .AddTransforms(transforms =>
    {
        transforms.AddRequestTransform(transform =>
        {
            var requestId = Guid.NewGuid().ToString();
            transform.ProxyRequest.Headers.Add("x-request-id", requestId);
            return ValueTask.CompletedTask;
        });
    });

var app = builder.Build();

app.MapGet("/", () => "API Gateway is running.");
app.MapReverseProxy();

app.Run();
