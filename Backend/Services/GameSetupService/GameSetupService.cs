using Backend.Domains.PlayerDomain;
using Backend.Services.DiceServices.Interfaces;
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
			// Make sure there are players in the list
			if (players.Count() < 1)
			{
				throw new Exception("Need players to roll");
			}

			var firstPlayer = RollForFirstPlayer(players);

			// Reorder list so firstPlayer is first, maintaining original order
			int firstPlayerIndex = players.IndexOf(firstPlayer);
			var orderedPlayers = players.Skip(firstPlayerIndex)
										.Concat(players.Take(firstPlayerIndex))
										.ToList();

			return orderedPlayers;
		}

		Player RollForFirstPlayer(List<Player> players)
		{
			Player result = null;
			var playersWithRolls = RollDieForEachPlayer(players);

			var highestRoll = playersWithRolls.Max(e => e.diceValue);
			if (playersWithRolls.Where(e => e.diceValue == highestRoll).Count() > 1)
			{
				result = RollForFirstPlayer(playersWithRolls.Where(e => e.diceValue == highestRoll).Select(e => e.player).ToList());
			}
			else
			{
				result = playersWithRolls.Where(e => e.diceValue == highestRoll).FirstOrDefault().player;
			}

			return result;
		}

		List<(Player player, int diceValue)> RollDieForEachPlayer(List<Player> players)
		{
			// Roll a die for each player
			var result = new List<(Player player, int diceValue)>();
			foreach (var player in players)
			{
				var DiceValue = DiceService.Roll();

				result.Add((player, DiceValue));
			}
			return result;
		}
	}
}
