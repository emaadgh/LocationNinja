namespace LocationNinja.Common;

public partial class Features
{
    public IpLocationSettings IpLocation { get; set; } = null!;
}
public class IpLocationSettings
{
    public string IpApiBaseUrl { get; set; } = null!;
}
