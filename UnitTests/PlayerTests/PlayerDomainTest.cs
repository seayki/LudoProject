using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.PlayerTests
{
    public class PlayerDomainTest
    {
        [Theory]
        [InlineData(0, ColourEnum.None, null, false, false, false)]
        [InlineData(1, ColourEnum.Red, null, false, false, true)]
        [InlineData(2, ColourEnum.Blue, null, false, false, true)]
        [InlineData(3, ColourEnum.Green, null, false, false, true)]
        [InlineData(4, ColourEnum.Yellow, null, false, false, true)]

        public void TestPlayerConstructor(
            int id,
            ColourEnum colour,
            List<Piece> pieces,
            bool expectedIsTurn,
            PosIndex? expectedStartTile,
            bool shouldPass)
        {
            // Arrange
            Player? player = null;
            Exception? caughtException = null;
            // Act
            try
            {
                player = new Player(id, colour, pieces);
            }
            catch (Exception ex)
            {
                caughtException = ex;
            }
            // Assert
            if (shouldPass)
            {
                Assert.NotNull(player);
                Assert.Null(caughtException);
                Assert.Equal(id, player.Id);
                Assert.Equal(colour, player.Colour);
                Assert.Equal(pieces, player.Pieces);
                Assert.Equal(expectedIsTurn, player.IsTurn);
                Assert.Equal(expectedStartTile, player.StartTile);
            }
            else
            {
                Assert.Null(player);
                Assert.NotNull(caughtException);
                Assert.IsType<Exception>(caughtException);
            }
        }
    }
}
