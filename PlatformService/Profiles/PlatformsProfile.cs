using AutoMapper;

using OriApps.UniCommand.PlatformService.Data;
using OriApps.UniCommand.PlatformService.Data.DTO;
using OriApps.UniCommand.PlatformService.Models;
using OriApps.UniCommand.PlatformService.Models.Cost;

namespace OriApps.UniCommand.PlatformService.Profiles;

public class PlatformsProfile : Profile
{
	public PlatformsProfile()
	{
		CreateMap<Platform, PlatformReadDTO>();
		
		CreateMap<PlatformCreateDTO, Platform>()
			.ForMember(dest => dest.Cost, opt => opt.MapFrom<CostTypeResolver>());

		CreateMap<PlatformReadDTO, PlatformPublishedDTO>();
		CreateMap<Platform, GrpcPlatformModel>()
			.ForMember(destination => destination.PlatformId, opt => opt.MapFrom(source => source.Id))
			.ForMember(destination => destination.Name, opt => opt.MapFrom(source => source.Name))
			.ForMember(destination => destination.Publisher, opt => opt.MapFrom(source => source.Publisher));
	}
}

public class CostTypeResolver(PlatformServiceDbContext context) : IValueResolver<PlatformCreateDTO, Platform, CostType>
{
	public CostType Resolve(PlatformCreateDTO source, Platform destination, CostType destMember, ResolutionContext context1)
	{
		return context.Set<CostType>().Find(source.CostTypeID);
	}
}