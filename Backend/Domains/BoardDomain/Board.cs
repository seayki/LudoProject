using Common.Enums;

namespace Backend.Domains.Board
{
    public class Board
    {

        public Board(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours)
        {
            
        }

        public required List<Tiles> Tiles { get; init; }
        public required Dictionary<ColourEnum, List<Tiles>> ColourTiles { get; init; }


    }

    public class Tiles
    {

    }
}
