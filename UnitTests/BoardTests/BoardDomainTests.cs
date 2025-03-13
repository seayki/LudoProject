using Backend.Domains.Board;
using Common.Enums;

namespace UnitTests.BoardTests
{
    public class BoardDomainTests
    {
        [Fact]
        public void Schould_get_a_board_object()
        {
            //Arange
            int length = 52;
            int zoneLength = 6;
            List<ColourEnum> players = new List<ColourEnum>() { ColourEnum.Red, ColourEnum.Blue, ColourEnum.Green, ColourEnum.Yellow };

            //Act
            var result = new Board(length, zoneLength, players);

            //Assert
            Assert.Equal(result.Tiles.Count, length);
            Assert.Equal(result.ColourTiles.Count, players.Count);
            Assert.Equal(result.ColourTiles.Values.Count, zoneLength);
        }

    }
}
