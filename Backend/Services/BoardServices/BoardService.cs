using Backend.Domains.Board;
using Backend.Domains.TileDomain;
using Backend.Services.BoardServices.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace Backend.Services.BoardServices
{
    public class BoardService : IBoardService
    {
        Task<Board> IBoardService.CreateBoard(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBoardService.GetGoalTilePieces(List<ColourTile> colourTilesOnBoard, ColourEnum colour, int pieceId)
        {
            throw new NotImplementedException();
        }

        Task<PosIndex> IBoardService.GetStartTilePos(List<Tile> tilesOnBoard, ColourEnum colour)
        {
            throw new NotImplementedException();
        }

        Task<PosIndex> IBoardService.GetTileEndPos(List<Tile> tilesOnBoard, PosIndex piecePosIndex, ColourEnum pieceColour, int diceRoll)
        {
            throw new NotImplementedException();
        }
    }
}
