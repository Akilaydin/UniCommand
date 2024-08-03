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
	}
}
