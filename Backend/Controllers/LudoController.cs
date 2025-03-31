using Backend.Domains.PieceDomain;
using Common.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LudoController(/*GameManager gameManager, */ILogger<LudoController> logger) : ControllerBase
	{
		public async Task<IActionResult> FindValidMoves(int diceroll)
		{
			throw new NotImplementedException();
		}

		public async Task<IActionResult> MoveSelectedPiece(Guid pieceID, PosIndex posIndex)
		{
			throw new NotImplementedException();
		}

		public async Task<IActionResult> StartGame(int PlayerNumber, int BoardSize)
		{
			throw new NotImplementedException();
		}
	}
}
