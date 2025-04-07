using Backend.Domains.PieceDomain;
using Backend.Services.PieceService.Interfaces;
using Common.DTOs;

namespace Backend.Services.PieceService
{
	public class PieceService : IPieceService
	{
		Piece IPieceService.MovePiece(Piece piece, PosIndex posIndex)
		{
			if (!piece.IsInPlay)
			{
				throw new Exception("Piece is in base, not on board");
			}
			else if (piece.IsFinished)
			{
				throw new Exception("Piece is in goal, and can't be moved anymore");
			}

			piece.PosIndex = posIndex;
			return piece;
		}

		Piece IPieceService.MovePieceOut(Piece piece, PosIndex posIndex)
		{
			if (piece.IsInPlay)
			{
				throw new Exception("Piece is already in play, please select a different piece");
			}

			piece.PosIndex = posIndex;
			piece.IsInPlay = true;
			return piece;
		}

		Piece IPieceService.ReturnPieceToBase(Piece piece)
		{
			if (!piece.IsInPlay)
			{
				throw new Exception("Piece is already in base, and can't be returned to where it already is");
			}

			piece.PosIndex = null;
			piece.IsInPlay = false;
			return piece;
		}
	}
}
