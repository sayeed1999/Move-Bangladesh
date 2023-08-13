namespace AuthService.API
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
        public JWT JWT { get; set; }
        public string ClientUrl { get; set; }
    }

    public class JWT
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}