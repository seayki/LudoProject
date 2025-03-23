using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class Tile
    {
        public PosIndex PosIndex { get; init; }
        public Dictionary<DirectionEnum, PosIndex> Directions { get; init; }
        public readonly ITileService tileService;

        public Tile(PosIndex posIndex, Dictionary<DirectionEnum, PosIndex> directions)
        {
            this.tileService = new TileService();
            this.PosIndex = posIndex;
            this.Directions = directions;
        }

    }
}
