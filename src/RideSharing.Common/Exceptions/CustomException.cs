namespace RideSharing.Common.Exceptions
{
	public class CustomException : Exception
	{
		public CustomException(string message, short status) : base(message)
		{
			Status = status;
		}

		public short Status { get; set; }
	}
}
