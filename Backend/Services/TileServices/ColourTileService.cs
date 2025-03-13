using Backend.Domains.TileDomain;
using Common.DTOs;

namespace Backend.Services.TileServices
{
    public class ColourTileService: IColourTileService
    {

        Task<bool> IColourTileService.CheckForGoal(PosIndex posIndex)
        {
            throw new NotImplementedException();
        }
    }
}
