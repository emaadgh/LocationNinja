namespace LocationNinja.Features.IpLocation.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, IpLocationResponse>();
        CreateMap<Location, IpLocationDetailedResponse>();

        CreateMap<IpLocationProviderResponse, Location>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Ip, opt => opt.Ignore());
        CreateMap<IpLocationProviderResponse, IpLocationResponse>();
        CreateMap<IpLocationProviderResponse, IpLocationDetailedResponse>();

        CreateMap<IpApiResponse, IpLocationProviderResponse>();
    }
}
