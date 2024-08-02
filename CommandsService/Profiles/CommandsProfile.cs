using AutoMapper;

using OriApps.UniCommand.CommandsService.Data.DTO;
using OriApps.UniCommand.CommandsService.Models;

namespace OriApps.UniCommand.CommandsService.Profiles;

public class CommandsProfile : Profile
{
	public CommandsProfile()
	{
		CreateMap<Command, CommandReadDTO>();
		
		CreateMap<CommandCreateDTO, Command>();
	}
}
