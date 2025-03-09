using Common.Enums;

namespace Backend.Domains.Board
{
    public class Board
    {

        public Board(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours)
        {
            
        }

        public List<TileDomain.TileDomain> Tiles { get; init; }
        public Dictionary<ColourEnum, List<TileDomain.ColourTileDomain>> ColourTiles { get; init; }


    }
}
