using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class Tile
    {
        public PosIndex posIndex;
        public Dictionary<DirectionEnum, PosIndex> directions = new Dictionary<DirectionEnum, PosIndex>();
        private readonly ITileService tileService;

        public Tile(ITileService tileService)
        {
            this.tileService = tileService;

        }
        
    }
}
