using Backend.Domains.Board;
using Backend.Domains.TileDomain;
using Backend.Services.BoardServices;
using Backend.Services.BoardServices.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace UnitTests.BoardTests
{
    public class BoardServiceTests
    {

        public BoardServiceTests()
        {

        }

        [Theory]
        [InlineData(ColourEnum.Red)]
        [InlineData(ColourEnum.Green)]
        [InlineData(ColourEnum.Blue)]
        [InlineData(ColourEnum.Yellow)]
        public async Task Schould_get_one_posIndex_for_the_colour_start_tile(ColourEnum colour)
        {
            var expectedResult = new PosIndex() { Index = 5, Colour = colour };
            var startTile = new ColourTileDomain() { colour = colour, isGoalTile = false, isStartTile = true };
            var tilesOnBoard = new List<ColourTileDomain> { startTile };
            for (int i = 0; i < 6; i++)
            {
                tilesOnBoard.Add(new ColourTileDomain() { colour = ColourEnum.Red, isGoalTile = false, isStartTile = false });
                tilesOnBoard.Add(new ColourTileDomain() { colour = ColourEnum.Green, isGoalTile = false, isStartTile = false });
                tilesOnBoard.Add(new ColourTileDomain() { colour = ColourEnum.Blue, isGoalTile = false, isStartTile = false });
                tilesOnBoard.Add(new ColourTileDomain() { colour = ColourEnum.Yellow, isGoalTile = false, isStartTile = false });
            }

            IBoardService boardService = new BoardService();
            var result = await boardService.GetStartTilePos(tilesOnBoard, colour);

            Assert.Equal(expectedResult, result);
        }
    }
}
