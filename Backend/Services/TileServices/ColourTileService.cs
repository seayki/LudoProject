using Backend.Domains.TileDomain;
using Common.DTOs;

namespace Backend.Services.TileServices
{
    public class ColourTileService:Tile, IColourTileService
    {
        IColourTileService 
        public ColourTileService(ITileService tileService) : base(tileService)
        {
        }

        Task<bool> IColourTileService.CheckForGoal(PosIndex posIndex)
        {
            throw new NotImplementedException();
        }
    }
}
