using AutoMapper;

using Grpc.Core;

using OriApps.UniCommand.PlatformService.Data;

namespace OriApps.UniCommand.PlatformService.Services;

public class GrpcPlatformService(IPlatformRepository platformRepository, IMapper mapper) : GrpcPlatform.GrpcPlatformBase
{
	public override async Task<PlatformResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
	{
		var response = new PlatformResponse();
		var platforms = await platformRepository.GetAllPlatformsAsync();

		foreach (var platform in platforms)
		{
			response.Platform.Add(mapper.Map<GrpcPlatformModel>(platform));
		}

		return response;
	}
}
