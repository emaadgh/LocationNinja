public class AppSettings
{
    public MongoDatabase MongoDatabase { get; set; } = null!;
    public BrokerOptions BrokerOptions { get; set; } = null!;
    public Features Features { get; set; } = null!;
}

public class MongoDatabase
{
    public string Host { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}
public sealed class BrokerOptions
{
    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public partial class Features
{

}
