namespace LocationNinja.Common;

public class AppSettings
{
    public MongoDatabase MongoDatabase { get; set; } = null!;
    public Features Features { get; set; } = null!;
}

public class MongoDatabase
{
    public string Host { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}

public partial class Features
{

}
