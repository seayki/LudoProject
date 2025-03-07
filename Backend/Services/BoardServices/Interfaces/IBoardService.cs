using Backend.Domains.Board;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices.Interfaces
{
    public interface IBoardService
    {
        Task<PosIndex> GetTileEndPos(List<Tiles> tilesOnBoard, PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll);
        Task<PosIndex> GetStartTilePos(List<Tiles> tilesOnBoard, ColourEnum colour);
        Task<bool> GetGoalTilePieces(List<Tiles> colourTilesOnBoard, ColourEnum colour, int pieceId);
        Task<Board> CreateBoard(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours);
    }
}
