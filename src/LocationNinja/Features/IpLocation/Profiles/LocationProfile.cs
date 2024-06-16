using AutoMapper;
using LocationNinja.Features.IpLocation.Domain;
using LocationNinja.Features.IpLocation.Models;
using LocationNinja.Features.IpLocation.Providers.IpApi;

namespace LocationNinja.Features.IpLocation.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, IpLocationResponse>();
        CreateMap<Location, IpLocationDetailedResponse>();

        CreateMap<IpLocationApiResponse, Location>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Ip, opt => opt.Ignore());
        CreateMap<IpLocationApiResponse, IpLocationResponse>();
        CreateMap<IpLocationApiResponse, IpLocationDetailedResponse>();

        CreateMap<IpApiResponse, IpLocationApiResponse>();
    }
}
