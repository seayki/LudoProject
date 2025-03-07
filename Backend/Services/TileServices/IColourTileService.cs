using Common.DTOs;

namespace Backend.Services.TileServices
{
    public interface IColourTileService
    {
        public Task<bool> CheckForGoal(PosIndex posIndex);
    }
}
