using Common.DTOs;
using Common.Enums;
using System.Drawing;

namespace Backend.Services.BoardServices.Interfaces
{
    public interface IBoardService
    {
        Task<PosIndex> GetTileEndPos(PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll);
        Task<PosIndex> GetStartTilePos(ColourEnum colour);
        Task<bool> GetGoalTilePieces(ColourEnum colour, int pieceId);
    }
}
