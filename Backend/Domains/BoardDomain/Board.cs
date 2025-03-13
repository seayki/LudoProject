using Backend.Domains.TileDomain;
using Backend.Services.TileServices;
using Common.DTOs;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Domains.Board
{
    public class Board
    {

        public Board(int numberOfTiles, int lengthOfColourZone, List<ColourEnum> playerColours)
        {
            for (int i = 0; i < numberOfTiles; i++)
            {
                if (Tiles.Count != 0)
                {
                    var indexRef = Tiles.ElementAt(i - 1).posIndex;
                }

                Tiles.Add(new Tile(new TileService(), new PosIndex() { Index = i, Colour = ColourEnum.None }, new Dictionary<DirectionEnum, PosIndex>()));
            }
            var lengthBetweenStartTiles = numberOfTiles / playerColours.Count;

            foreach (var player in playerColours)
            {
                var tempColourTiles = new List<ColourTile>();
                for (int i = 0; i < lengthOfColourZone; i++)
                {
                    var tempColourTile = new ColourTile(new TileService(), new PosIndex() { Colour = player, Index = i}, new Dictionary<DirectionEnum, PosIndex>(), new ColourTileService());
                    tempColourTiles.Add(tempColourTile);
                }
                var goalTile = tempColourTiles.Last();
                goalTile.isGoalTile = true;
                tempColourTiles.Insert(tempColourTiles.Count - 1, goalTile);

                ColourTiles.Add(player, tempColourTiles);
            }
        }

        public List<Tile> Tiles { get; init; } = new();
        public Dictionary<ColourEnum, List<ColourTile>> ColourTiles { get; init; } = new();


    }
}
