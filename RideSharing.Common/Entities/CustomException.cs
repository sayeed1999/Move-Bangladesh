namespace RideSharing.Common.Entities
{
    public class CustomException : Exception
    {
        public CustomException(string message, short status) : base(message)
        {
            this.Status = status;
        }

        public short Status { get; set; }
    }
}