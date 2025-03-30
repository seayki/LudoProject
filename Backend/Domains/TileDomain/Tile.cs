using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.TileDomain
{
    public class Tile
    {
        public PosIndex PosIndex { get; init; }
        public ColourEnum Colour { get; set; }
        public Dictionary<DirectionEnum, PosIndex> Directions { get; init; }
        public bool IsGoalTile { get; set; }
        public bool IsStartTile { get; set; }

        public Tile(PosIndex posIndex, ColourEnum colour, Dictionary<DirectionEnum, PosIndex> directions, bool isGoalTile, bool isStartTile)
        {
            PosIndex = posIndex;
            Colour = colour;
            Directions = directions;
            IsGoalTile = isGoalTile;
            IsStartTile = isStartTile;
        }
        
    }
}
