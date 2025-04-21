using Backend.Domains.BoardDomain;
using Backend.Domains.PieceDomain;
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
            var result = new Board(length, zoneLength, players, new List<Piece>());

            //Assert
            Assert.Equal(length, result.Tiles.Count);
            Assert.Equal(players.Count, result.PlayerZones.Count);
            Assert.True(result.PlayerZones.Values.All(x => x.Count == zoneLength));
            Assert.True(result.PlayerZones.Values.All(x => x.Last().IsGoalTile == true));
        }

    }
}
