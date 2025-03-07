using Backend.Domains.Board;
using Backend.Domains.TileDomain;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices.Interfaces
{
    public interface IBoardService
    {
        Task<PosIndex> GetTileEndPos(List<TileDomain> tilesOnBoard, PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll);
        Task<PosIndex> GetStartTilePos(List<ColourTileDomain> tilesOnBoard, ColourEnum colour);
        Task<bool> GetGoalTilePieces(List<ColourTileDomain> colourTilesOnBoard, ColourEnum colour, int pieceId);
        Task<Board> CreateBoard(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours);
    }
}
