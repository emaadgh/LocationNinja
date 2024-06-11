namespace LocationNinja.Features.IpLocation.Providers.IpApi;

public class IpApiNullResponseException : Exception
{
    private const string _message = "ip-api Http response is null.";
    public IpApiNullResponseException() : base(_message) { }
}
