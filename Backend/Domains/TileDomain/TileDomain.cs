using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class TileDomain
    {
        public PosIndex posIndex;
        public Dictionary<DirectionEnum, PosIndex> directions = new Dictionary<DirectionEnum, PosIndex>();
        private readonly ITileService tileService;

        public TileDomain(ITileService tileService)
        {
            this.tileService = tileService;
        }
        
    }
}
