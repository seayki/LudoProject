using Backend.Domains.Board;
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
        public async Task Schould_get_one_posIndex_for_the_colour_start_tile(ColourEnum colour)
        {
            var expectedResult = new PosIndex() { Index = 5, Colour = ColourEnum.Red};

            var tilesOnBoard = new List<Tiles>();
            IBoardService boardService = new BoardService();
            var result = await boardService.GetStartTilePos(tilesOnBoard, colour);

            Assert.Equal(expectedResult, result);
        }

    }
}
