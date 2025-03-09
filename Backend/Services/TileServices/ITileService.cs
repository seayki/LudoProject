using Common.DTOs;
using Common.Enums;

namespace Backend.Services.TileServices
{
    public interface ITileService
    {
        Task<PosIndex> CheckNextTile(PosIndex currentPosIndex, ColourEnum pieceColor);
        

    }
}
