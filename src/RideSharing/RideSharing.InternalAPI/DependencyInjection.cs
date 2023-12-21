using RideSharing.API.MessageQueues.Actions;
using RideSharing.API.MessageQueues.Receiver;

namespace RideSharing.API;

public static class DependencyInjection
{
    public static void RegisterRabbitMQToApplication(this WebApplication app)
    {
        // rabbitmq emitter configs
        var userRegisteredConsumer = new UserRegisteredConsumer();
        var userModifierConsumer = new UserModifiedConsumer();

        var scope = app.Services.CreateScope();

        var actions = scope.ServiceProvider.GetRequiredService<Actions>();
        userRegisteredConsumer.Start(actions.OnUserRegistered);
        userModifierConsumer.Start(actions.OnUserModified);

        // stopping rabbitmq instances
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStopping.Register(() =>
        {
            userRegisteredConsumer.Stop();
            userModifierConsumer.Stop();
            scope.Dispose();
        });
    }
}
