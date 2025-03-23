using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class ColourTile : Tile
    {
        public ColourEnum Colour { get; init; }
        public bool IsGoalTile { get; set; }
        private readonly IColourTileService colourTileService;

        public ColourTile(ColourEnum colour, bool isGoalTile, bool isStartTile, PosIndex posIndex, Dictionary<DirectionEnum, PosIndex> directions) : base(posIndex, directions)
        {
            this.Colour = colour;
            this.IsGoalTile = isGoalTile;
            this.colourTileService = new ColourTileService();
        }
    }
}
