using Common.DTOs;

namespace Backend.Services.TileServices
{
    public interface IColourTileService
    {
        Task<bool> CheckForGoal(PosIndex posIndex);
    }
}
