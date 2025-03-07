using Backend.Domains.PieceDomain;
using Backend.Services.PieceService.Interfaces;
using Common.DTOs;

namespace Backend.Services.PieceService
{
	public class PieceService : IPieceService
	{
		Piece IPieceService.MovePiece(Piece piece, PosIndex posIndex)
		{
			throw new NotImplementedException();
		}

		Piece IPieceService.MovePieceOut(Piece piece, PosIndex posIndex)
		{
			throw new NotImplementedException();
		}

		Piece IPieceService.ReturnPieceToBase(Piece piece)
		{
			throw new NotImplementedException();
		}
	}
}
