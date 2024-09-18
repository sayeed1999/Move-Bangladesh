using Microsoft.Extensions.DependencyInjection;
using MoveBangladesh.Domain.Entities;
using MoveBangladesh.Processor.TransitionChecker;

namespace MoveBangladesh.Processor;

public static class DependencyInjection
{
	public static IServiceCollection RegisterMoveBangladeshProcessor(this IServiceCollection services)
		=> services
			.AddScoped<ITransitionChecker<TripStatus>, TripStatusTransitionChecker>()
			.AddScoped<ITransitionChecker<TripRequestStatus>, TripRequestStatusTransitionChecker>();
}
