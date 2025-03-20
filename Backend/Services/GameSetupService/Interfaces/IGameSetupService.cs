using Backend.Domains.PlayerDomain;
using Backend.Services.DiceService.Interfaces;

namespace Backend.Services.GameSetupService.Interfaces
{
	public interface IGameSetupService
	{
		List<Player> RollForPlayerOrder(List<Player> players);
	}
}
