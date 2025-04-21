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
		[HttpGet("FindValidMoves")]
		public async Task<IActionResult> FindValidMoves(int diceroll)
		{
			try
			{
				gameManager.Roll(diceroll);
				var resultValue = gameManager.GetPossibleMoves();

				return new OkObjectResult(resultValue);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error");
				return new BadRequestObjectResult("An exception occurred, please check logs");
			}
		}

		[HttpGet("MoveSelectedPiece")]
		public async Task<IActionResult> MoveSelectedPiece(Guid pieceID, PosIndex posIndex)
		{
			try
			{
				var affectedPieces = gameManager.MovePiece(pieceID);
				return new OkObjectResult(affectedPieces);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error");
				return new BadRequestObjectResult("An exception occurred, please check logs");
			}
		}

		[HttpGet("StartGame")]
		public async Task<IActionResult> StartGame(int PlayerNumber, int BoardSize)
		{
			try
			{
				gameManager.CreateNewGame(PlayerNumber, BoardSize, 6);
				var playersInOrder = gameManager.RollForPlayerOrder();

				var resultValue = from p in playersInOrder
								  select new PlayerDTO()
								  {
									  id = p.Id,
									  colour = p.Colour,
									  startTile = p.StartTile,
									  pieces = (List<PieceDTO>)(from piece in p.Pieces
											   select new PieceDTO()
											   {
												   ID = piece.ID,
												   PosIndex = piece.PosIndex,
												   IsInPlay = piece.IsInPlay,
												   IsFinished = piece.IsFinished
											   })
								  };

				return new OkObjectResult(resultValue);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error");
				return new BadRequestObjectResult("An exception occurred, please check logs");
			}
		}

		[HttpGet("NextTurn")]
		public async Task<IActionResult> NextTurn()
		{
			try
			{
				var nextPlayer = gameManager.NextTurn();
				return new OkObjectResult(nextPlayer);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error");
				return new BadRequestObjectResult("An exception occurred, please check logs");
			}
		}
	}
}
