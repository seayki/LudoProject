using Common.Enums;

namespace Backend.Domains.Board
{
    public class Board
    {

        public Board(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours)
        {
            
        }

        public required List<TileDomain.TileDomain> Tiles { get; init; }
        public required Dictionary<ColourEnum, List<TileDomain.ColourTileDomain>> ColourTiles { get; init; }


    }
}
