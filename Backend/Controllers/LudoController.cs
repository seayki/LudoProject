using Backend.Domains.GameManagerDomain;
using Backend.Domains.PieceDomain;
using Backend.Services.DiceServices.Interfaces;
using Backend.Services.GameManagerService;
using Common.DTOs;
using Common.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LudoController : ControllerBase
	{
        private readonly IGameManagerService gameManager;
        private readonly ILogger<LudoController> logger;
		private readonly IDiceService diceService;

        public LudoController(IGameManagerService gameManager, IDiceService diceService, ILogger<LudoController> logger)
        {
            this.gameManager = gameManager;
            this.logger = logger;
			this.diceService = diceService;
        }

        [HttpGet("FindValidMoves")]
		public async Task<IActionResult> RollDieAndFindValidMoves()
		{
			try
			{
				int diceroll = diceService.Roll();
				gameManager.Roll(diceroll);
				var validPieces = gameManager.GetMovablePieces();
				var resultValue = new RollDieAndFindValidMovesResponseDTO 
				{ 
					diceroll = diceroll, 
					validPieces = validPieces 
				};

				return new OkObjectResult(resultValue);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Error");
				return new BadRequestObjectResult("An exception occurred, please check logs");
			}
		}

		[HttpGet("MoveSelectedPiece")]
		public async Task<IActionResult> MoveSelectedPiece(Guid pieceID)
		{
			try
			{
				var affectedPieces = gameManager.MovePiece(pieceID);

				var resultValue = from p in affectedPieces
								  select new PieceDTO()
								  {
									  ID = p.ID,
									  PosIndex = p.PosIndex,
									  IsInPlay = p.IsInPlay,
									  IsFinished = p.IsFinished
								  };

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
				var playersInOrder = gameManager.RollForPlayerOrder();
				gameManager.CreateNewGame(PlayerNumber, BoardSize, 6);

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
                                      }).ToList()
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
