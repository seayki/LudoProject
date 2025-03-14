using Backend.Services.BoardServices.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices
{
    public class BoardService : IBoardService
    {
        public Task<bool> GetGoalTilePieces(ColourEnum colour, int pieceId)
        {
            throw new NotImplementedException();
        }

        public Task<PosIndex> GetStartTilePos(ColourEnum colour)
        {
            throw new NotImplementedException();
        }

        public Task<PosIndex> GetTileEndPos(PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll)
        {
            throw new NotImplementedException();
        }
    }
}
