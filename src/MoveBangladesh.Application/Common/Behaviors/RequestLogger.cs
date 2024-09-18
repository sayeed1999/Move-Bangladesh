using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using MoveBangladesh.Application.Abstractions;

namespace MoveBangladesh.Application.Common.Behaviors
{
	// TODO:- Add user service and sync here
	public class RequestLogger<TRequest>(
		ILogger<TRequest> logger,
		IUserContext userContext)
		: IRequestPreProcessor<TRequest>
	{
		public Task Process(TRequest request, CancellationToken cancellationToken)
		{
			var name = typeof(TRequest).Name;

			logger.LogInformation("Northwind Request: {Name} {@UserId} {@Request}",
				name,
				userContext.UserId,
				request);

			return Task.CompletedTask;
		}
	}
}
