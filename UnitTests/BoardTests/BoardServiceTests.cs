using Backend.Domains.BoardDomain;
using Backend.Services.BoardServices;
using Backend.Services.BoardServices.Interfaces;
using Common.Enums;

namespace UnitTests.BoardTests
{
    public class BoardServiceTests
    {

        [Theory]
        [InlineData(ColourEnum.Red)]
        [InlineData(ColourEnum.Green)]
        [InlineData(ColourEnum.Blue)]
        [InlineData(ColourEnum.Yellow)]
        public void Schould_get_one_posIndex_for_the_colour_start_tile(ColourEnum colour)
        {
            //Arange
            var board = new Board(52, 6, new List<ColourEnum>() { ColourEnum.Red, ColourEnum.Blue, ColourEnum.Green, ColourEnum.Yellow });
            IBoardService boardService = new BoardService();

            //Act
            var result = boardService.GetStartTilePos(board.Tiles, colour);

            //Assert
            Assert.True(result == board.Tiles.Find(x => x.Colour == colour)!.PosIndex);
        }
    }
}
