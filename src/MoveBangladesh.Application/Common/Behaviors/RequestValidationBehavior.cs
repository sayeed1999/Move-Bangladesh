using FluentValidation;
using MediatR;

namespace MoveBangladesh.Application.Common.Behaviors
{
	public class RequestValidationBehavior<TRequest, TResponse>(
		IEnumerable<IValidator<TRequest>> validators)
		: IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);

			var failures = validators
				.Select(v => v.Validate(context))
				.SelectMany(result => result.Errors)
				.Where(f => f != null)
				.ToList();

			if (failures.Count != 0)
			{
				throw new Exception(failures[0].ErrorMessage);
			}

			return next();
		}
	}
}
