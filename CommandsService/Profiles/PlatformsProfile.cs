using AutoMapper;

using OriApps.UniCommand.CommandsService.Data.DTO;
using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Profiles;

public class PlatformsProfile : Profile
{
	public PlatformsProfile()
	{
		CreateMap<Platform, PlatformReadDTO>();
		CreateMap<PlatformPublishedDTO, Platform>()
			.ForMember(to => to.ExtermalId, 
				opt => opt.MapFrom(from => from.Id));
		CreateMap<GrpcPlatformModel, Platform>()
			.ForMember(source => source.ExtermalId, opt => opt.MapFrom(source => source.PlatformId)) 
			.ForMember(source => source.Name, opt => opt.MapFrom(source => source.Name))
			.ForMember(source => source.Commands, opt => opt.Ignore());
	}
}
