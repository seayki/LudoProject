using Backend.Domains.TileDomain;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.Board
{
    public class Board
    {
        public List<Tile> Tiles { get; init; } = new();
        public Dictionary<ColourEnum, List<ColourTile>> ColourTiles { get; init; } = new();
        public List<ColourTile> StartTiles { get; set; } = new();

        public Board(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours)
        {
            for (int i = 0; i < numberOfTiles; i++)
            {
                var index = new PosIndex(i, ColourEnum.None);

                Tiles.Add(new Tile(index, new Dictionary<DirectionEnum, PosIndex>()));
            }

            for (int i = 0; i < Tiles.Count; i++)
            {
                if (i == Tiles.Count - 1)
                {
                    Tiles.ElementAt(i).Directions.Add(DirectionEnum.Forward, Tiles.First().PosIndex);
                }
                else
                {
                    Tiles.ElementAt(i).Directions.Add(DirectionEnum.Forward, Tiles.ElementAt(i + 1).PosIndex);
                }
            }

            foreach (var playerColour in playerColours)
            {
                var colourTiles = new List<ColourTile>();
                for (int i = 0; i < lengthOfColourZone; i++)
                {
                    var colourTile = new ColourTile(playerColour, false, false, new PosIndex(i, playerColour), new Dictionary<DirectionEnum, PosIndex>());
                    colourTiles.Add(colourTile);
                }

                colourTiles.Last().IsGoalTile = true;

                for (int i = 0; i < ColourTiles.Count; i++)
                {
                    if (i == 0)
                    {
                        colourTiles.ElementAt(i).Directions.Add(DirectionEnum.Forward, Tiles.ElementAt(i + 1).PosIndex);
                    }
                    else if (i == ColourTiles.Count - 1)
                    {
                        colourTiles.ElementAt(i).Directions.Add(DirectionEnum.Backward, Tiles.ElementAt(i - 1).PosIndex);
                    }
                    else
                    {
                        colourTiles.ElementAt(i).Directions.Add(DirectionEnum.Forward, Tiles.ElementAt(i + 1).PosIndex);
                        colourTiles.ElementAt(i).Directions.Add(DirectionEnum.Backward, Tiles.ElementAt(i - 1).PosIndex);
                    }
                }

                ColourTiles.Add(playerColour, colourTiles);
            }

            int lengthBetweenStartTiles = numberOfTiles / playerColours.Count;

            for (int i = 0; i < playerColours.Count; i++)
            {
                var result = ColourTiles.TryGetValue(playerColours.ElementAt(i), out var colourZone);
                var tile = Tiles.ElementAt(lengthBetweenStartTiles * i);
                tile.Directions.Add(DirectionEnum.ToColourZone, colourZone!.First().PosIndex);
                StartTiles.Add(new ColourTile(playerColours.ElementAt(i), false, true, tile.PosIndex, tile.Directions));
                Tiles.RemoveAt(i);
            }
        }
    }
}
