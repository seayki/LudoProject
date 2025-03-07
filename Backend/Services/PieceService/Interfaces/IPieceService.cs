using Backend.Domains.PieceDomain;
using Common.DTOs;

namespace Backend.Services.PieceService.Interfaces
{
	public interface IPieceService
	{
		Piece MovePiece(Piece piece, PosIndex posIndex);
		Piece MovePieceOut(Piece piece, PosIndex posIndex);
		Piece ReturnPieceToBase(Piece piece);
	}
}
