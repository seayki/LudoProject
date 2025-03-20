using Backend.Domains.PlayerDomain;
using Backend.Services.DiceService.Interfaces;
using Backend.Services.GameSetupService.Interfaces;

namespace Backend.Services.GameSetupService
{
	public class GameSetupService : IGameSetupService
	{
		public GameSetupService(IDiceService diceService)
		{
			DiceService = diceService;
		}

		public IDiceService DiceService { get; }

		List<Player> IGameSetupService.RollForPlayerOrder(List<Player> players)
		{
			throw new NotImplementedException();
		}
	}
}
