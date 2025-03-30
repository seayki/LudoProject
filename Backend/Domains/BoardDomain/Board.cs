using Backend.Domains.PieceDomain;
using Backend.Domains.TileDomain;
using Backend.Services.BoardServices;
using Backend.Services.BoardServices.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.Board
{
    public class Board
    {
        public List<Tile> Tiles { get; init; } = new();
        public Dictionary<ColourEnum, List<Tile>> PlayerZones { get; init; } = new();
        public List<Piece> Pieces { get; set; } = new();
        public IBoardService BoardService { get; init; } = new BoardService();
        private int NumberOfTiles { get; init; }
        private int LengthOfColourZone { get; init; }

        public Board(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours)
        {
            NumberOfTiles = numberOfTiles;
            LengthOfColourZone = lengthOfColourZone;

            for (int i = 0; i < numberOfTiles; i++)
            {
                var index = new PosIndex(i, ColourEnum.None);

                Tiles.Add(new Tile(index, ColourEnum.None, new Dictionary<DirectionEnum, PosIndex>(), false, false));
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
                var playerTiles = new List<Tile>();
                for (int i = 0; i < lengthOfColourZone; i++)
                {
                    var playerTile = new Tile(new PosIndex(i, playerColour), playerColour, new Dictionary<DirectionEnum, PosIndex>(), false, false);
                    playerTiles.Add(playerTile);
                }

                playerTiles.Last().IsGoalTile = true;

                for (int i = 0; i < PlayerZones.Count; i++)
                {
                    if (i == 0)
                    {
                        playerTiles.ElementAt(i).Directions.Add(DirectionEnum.Forward, Tiles.ElementAt(i + 1).PosIndex);
                    }
                    else if (i == PlayerZones.Count - 1)
                    {
                        playerTiles.ElementAt(i).Directions.Add(DirectionEnum.Backward, Tiles.ElementAt(i - 1).PosIndex);
                    }
                    else
                    {
                        playerTiles.ElementAt(i).Directions.Add(DirectionEnum.Forward, Tiles.ElementAt(i + 1).PosIndex);
                        playerTiles.ElementAt(i).Directions.Add(DirectionEnum.Backward, Tiles.ElementAt(i - 1).PosIndex);
                    }
                }

                PlayerZones.Add(playerColour, playerTiles);
            }

            int lengthBetweenStartTiles = numberOfTiles / playerColours.Count;

            for (int i = 0; i < playerColours.Count; i++)
            {
                var result = PlayerZones.TryGetValue(playerColours.ElementAt(i), out var colourZone);
                if (!result)
                {
                    throw new Exception("Could not find colour zone");
                }

                int element = i * lengthBetweenStartTiles;

                Tiles.ElementAt(element).IsStartTile = true;
                Tiles.ElementAt(element).Colour = colourZone!.First().Colour;

                if (element == 0)
                {
                    int indexOfLast = Tiles.IndexOf(Tiles.Last());
                    Tiles.ElementAt(indexOfLast - 1).Directions.Add(DirectionEnum.ToColourZone, colourZone!.First().PosIndex);
                }
                else
                {
                    Tiles.ElementAt(element - 2).Directions.Add(DirectionEnum.ToColourZone, colourZone!.First().PosIndex);
                }
            }
        }

        public async Task<PosIndex> GetStartTile(ColourEnum colour)
        {
            var result = await BoardService.GetStartTilePos(Tiles, colour);
            return result;
        }




    }
}
