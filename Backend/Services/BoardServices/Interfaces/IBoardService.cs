using Backend.Domains.Board;
using Backend.Domains.PieceDomain;
using Backend.Domains.TileDomain;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices.Interfaces
{
    public interface IBoardService
    {
        PosIndex GetStartTilePos(List<Tile> tiles, ColourEnum colour);
        List<Piece>? FindValidPicesToMove(List<Piece> pieces, ColourEnum colour, int diceRoll, List<Tile> tiles, List<Tile> playerZone);
        List<Piece> MovePiece(List<Piece> pieces, Piece piece, ColourEnum colour, int diceRoll, List<Tile> tiles, List<Tile> playerZone);
        void SendPieceHome(Piece piece);
    }
}
