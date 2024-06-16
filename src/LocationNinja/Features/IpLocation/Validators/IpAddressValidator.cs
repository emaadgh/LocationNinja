using FluentValidation;

namespace LocationNinja.Features.IpLocation.Validators;

public class IpAddressValidator : AbstractValidator<string>
{
    private const string IpAddressRequiredMessage = "IP address is required.";
    private const string InvalidIpAddressMessage = "Invalid IP address format.";
    public IpAddressValidator()
    {
        RuleFor(ip => ip)
            .NotEmpty().WithMessage(IpAddressRequiredMessage)
            .Must(IsValidIpAddress).WithMessage(InvalidIpAddressMessage);
    }

    private bool IsValidIpAddress(string ipAddress)
    {
        return System.Net.IPAddress.TryParse(ipAddress, out _);
    }
}
