namespace RideSharing.API
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public string JwtSecretKey { get; set; }
        public string ClientUrl { get; set; }
    }
}