using System.Text.RegularExpressions;
using RideSharing.Domain.Common;

namespace RideSharing.Domain.ValueObjects
{
	public class Email : ValueObject
	{
		public string Value { get; }

		public Email(string emailAddress)
		{
			if (!IsValid(emailAddress))
			{
				throw new ArgumentException("Invalid email address.", nameof(emailAddress));
			}

			Value = emailAddress;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Value.ToLowerInvariant();
		}

		public static bool IsValid(string emailAddress)
		{
			// Add your email validation logic here. For a simple example, you can use regular expressions.
			// In a real application, you should use a more robust validation method.
			// Here, we're using a basic regex pattern.
			string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
			return Regex.IsMatch(emailAddress, pattern);
		}
	}
}
