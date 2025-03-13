using Common.DTOs;
using Common.Enums;

namespace Backend.Services.TileServices
{
    public class TileService : ITileService
    {
        Task<PosIndex> ITileService.CheckNextTile(PosIndex currentPosIndex, ColourEnum pieceColor, Dictionary<DirectionEnum, PosIndex> directions, DirectionEnum direction)
        {
            DirectionEnum currentDirection = direction;

            if(currentPosIndex.Colour==pieceColor && currentDirection!=DirectionEnum.Backward)
            {
                currentDirection = DirectionEnum.GoToColourTiles;
            }

            PosIndex tileToMoveTo = directions[currentDirection];
            

            return Task.FromResult(tileToMoveTo);
                     
        }
    }
}
