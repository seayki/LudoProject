using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class ColourTile:Tile
    {
        public ColourEnum colour;
        public bool isGoalTile;
        public bool isStartTile;
        private readonly IColourTileService colourTileService;
        
        public ColourTile(ITileService tileService,PosIndex posIndex, Dictionary<DirectionEnum, PosIndex> directions, IColourTileService colourTileService):base(tileService,posIndex,directions)
        {
            this.colourTileService = colourTileService;
        }
        

    }
}
