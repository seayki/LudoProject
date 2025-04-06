using Backend.Domains.PlayerDomain;

namespace Backend.Services.GameSetupService.Interfaces
{
	public interface IGameSetupService
	{
		List<Player> RollForPlayerOrder(List<Player> players);
	}
}
