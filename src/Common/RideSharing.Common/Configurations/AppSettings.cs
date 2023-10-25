namespace RideSharing.Common.Configurations;

public class AppSettings
{
    public ConnectionStrings ConnectionStrings { get; set; }
    public ClientApplication ClientApplication { get; set; }
    public RedisServer RedisServer { get; set; }
    public SmtpServer SmtpServer { get; set; }
}

public class ConnectionStrings
{
    public string DatabaseConnectionString { get; set; }
}

public class ClientApplication
{
    public string AllowedOrigins { get; set; }
}

public class RedisServer
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public string Port { get; set; }
}

public class SmtpServer
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public string Port { get; set; }
    public string Email { get; set; }
}
