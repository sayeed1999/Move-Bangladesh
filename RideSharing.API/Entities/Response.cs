namespace RideSharing.API
{
    public class Response<T> where T : class
    {
        public T Data { get; set; }
        public string Message { get; set; } = "Success!";
        public short Status { get; set; } = 200;
    }

}
