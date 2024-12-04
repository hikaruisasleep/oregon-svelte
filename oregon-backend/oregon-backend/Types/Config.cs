namespace oregon_backend.types;

public class Config
{
    public Database Database { get; set; }
    public string JwtSecret { get; set; }
}

public class Database
{
    public string Db { get; set; }
    public string Host { get; set; }
    public string Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public bool TrustServerCertificate { get; set; }
}