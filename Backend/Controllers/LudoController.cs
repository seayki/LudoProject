using Backend.Domains.PieceDomain;
using Backend.Services.GameManagerServicesTemp.Interfaces;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LudoController(IGameManagerService gameManager, ILogger<LudoController> logger) : ControllerBase
	{
		public async Task<IActionResult> FindValidMoves(int diceroll)
		{
			try
			{
				var resultValue = gameManager.GetPossibleMoves();

				return new OkObjectResult(resultValue);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error");
				return new BadRequestObjectResult("An exception occurred, please check logs");
			}
		}

		public async Task<IActionResult> MoveSelectedPiece(Guid pieceID, PosIndex posIndex)
		{
			throw new NotImplementedException();
		}

		public async Task<IActionResult> StartGame(int PlayerNumber, int BoardSize)
		{
			try
			{
				var NewGameData = gameManager.CreateNewGame(PlayerNumber, BoardSize, 6);
				gameManager.RollForPlayerOrder();

				var resultValue = from p in NewGameData.Players
								  select new PlayerDTO()
								  {
									  id = p.Id,
									  colour = p.Colour,
									  startTile = p.StartTile
								  };

				return new OkObjectResult(resultValue);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error");
				return new BadRequestObjectResult("An exception occurred, please check logs");
			}
		}
	}
}
