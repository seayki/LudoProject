using Common.DTOs;
using Common.Enums;

namespace Backend.Services.TileServices
{
    public class TileService : ITileService
    {
        Task<PosIndex> ITileService.CheckNextTile(PosIndex currentPosIndex, ColourEnum pieceColor)
        {
            throw new NotImplementedException();
        }
    }
}
