using Microsoft.Extensions.DependencyInjection;
using RideSharing.Domain.Entities;
using RideSharing.Processor.TransitionChecker;

namespace RideSharing.Processor;

public static class DependencyInjection
{
	public static IServiceCollection RegisterRideSharingProcessor(this IServiceCollection services)
		=> services
			.AddScoped<ITransitionChecker<TripStatus>, TripStatusTransitionChecker>()
			.AddScoped<ITransitionChecker<TripRequestStatus>, TripRequestStatusTransitionChecker>();
}
