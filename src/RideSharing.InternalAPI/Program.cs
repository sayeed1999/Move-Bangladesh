var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "RideSharing.Internal API is running!");

app.Run();
