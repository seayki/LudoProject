using Backend.Domains.Board;
using Backend.Domains.PieceDomain;
using Backend.Domains.TileDomain;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices.Interfaces
{
    public interface IBoardService
    {
        Task<PosIndex> GetTileEndPos(List<Tile> tiles, PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll);
        Task<PosIndex> GetStartTilePos(List<Tile> tiles, ColourEnum colour);
        Task<bool> GetPiecesInGoal(List<Tile> tiles, ColourEnum colour, int pieceId);
        Task<List<Piece>> FindValidPicesToMove(List<Piece> pieces, ColourEnum colour, int diceRoll, List<Tile> tiles, List<Tile> playerZone);
        Task<Piece> MovePiece(Piece piece);
    }
}
