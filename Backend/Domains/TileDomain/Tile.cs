using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class Tile
    {
        public PosIndex posIndex;
        public Dictionary<DirectionEnum, PosIndex> directions = new Dictionary<DirectionEnum, PosIndex>();
        public readonly ITileService tileService;

        public Tile(ITileService tileService,PosIndex posIndex, Dictionary<DirectionEnum, PosIndex> directions)
        {
            this.tileService = tileService;
            this.posIndex = posIndex;
            this.directions = directions;
        }
        
    }
}
