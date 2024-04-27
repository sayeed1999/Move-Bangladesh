using MediatR;
using Microsoft.Extensions.Logging;
using RideSharing.Application.Abstractions;
using System.Diagnostics;

namespace RideSharing.Application.Common.Behaviors
{
	public class RequestPerformanceBehaviour<TRequest, TResponse>(
		ILogger<TRequest> logger,
		IUserContext userContext)
		: IPipelineBehavior<TRequest, TResponse>
	{
		private readonly Stopwatch _timer = new();

		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			_timer.Start();

			var response = await next();

			_timer.Stop();

			if (_timer.ElapsedMilliseconds > 500)
			{
				var name = typeof(TRequest).Name;

				logger.LogWarning("Northwind Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
					name,
					_timer.ElapsedMilliseconds,
					userContext.UserId,
					request);
			}

			return response;
		}
	}
}
