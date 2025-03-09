using Backend.Domains.PlayerDomain;
using Common.Enums;
using Backend.Domains.PieceDomain;
using Common.DTOs;
using System.Security.Cryptography;

namespace UnitTests.PlayerTests
{
    public class PlayerDomainTest
    {
        [Theory]
        [InlineData(0, ColourEnum.None, false, null, false)]
        [InlineData(1, ColourEnum.Red, false, null, true)]
        [InlineData(2, ColourEnum.Blue, false, null, true)]
        [InlineData(3, ColourEnum.Green, false, null, true)]
        [InlineData(4, ColourEnum.Yellow, false, null, true)]

        public void TestPlayerConstructor(
            int id,
            ColourEnum colour,
            //List<Piece> pieces,
            bool expectedIsTurn,
            PosIndex? expectedStartTile,
            bool shouldPass)
        {
            // Arrange
            var pieces = new List<Piece>();
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

        // Test om brikker kan flyttes ud
        [Fact]
        public void TestPlayerCanMovePieceOutFromStart()
        {
            var player = new Player(1, ColourEnum.Red, new List<Piece>
            {
                new Piece(1, ColourEnum.Red)
                { IsInPlay = false },
                new Piece(2, ColourEnum.Red)
                { IsInPlay = false },
                new Piece(3, ColourEnum.Red)
                { IsInPlay = false },
                new Piece(4, ColourEnum.Red)
                { IsInPlay = true }
            });

            var moveablePieces = player.Pieces.Where(p => !p.IsInPlay).ToList();

            Assert.Equal(2, moveablePieces.Count); //Burde give 3
        }

        // Test at ingen brikker kan flyttes ud, hvis de er i spil.
        [Fact]
        public void TestPlayerCannotMoveAnyPieceOutFromStart()
        {
            var player = new Player(2, ColourEnum.Blue, new List<Piece>
            {
                new Piece(1, ColourEnum.Blue)
                { IsInPlay = true },
                new Piece(2, ColourEnum.Blue)
                { IsInPlay = true },
                new Piece(3, ColourEnum.Blue)
                { IsInPlay = true },
                new Piece(4, ColourEnum.Blue)
                { IsInPlay = true }
            });

            var moveablePieces = player.Pieces.Where(p => !p.IsInPlay).ToList();

            Assert.Empty(moveablePieces);
        }

        // Test for at sikre at kun en brik kan flyttes ud, hvis kun en brik ikke er i spil
        [Fact]
        public void TestPlayerHasOnePieceThatCanMoveOutFromStart()
        {
            var player = new Player(3, ColourEnum.Green, new List<Piece>
            {
                new Piece(1, ColourEnum.Green)
                { IsInPlay = true },
                new Piece(2, ColourEnum.Green)
                { IsInPlay = true },
                new Piece(3, ColourEnum.Green)
                { IsInPlay = true },
                new Piece(4, ColourEnum.Green)
                { IsInPlay = false }
            });

            var moveablePieces = player.Pieces.Where(p => !p.IsInPlay).ToList();

            Assert.Single(moveablePieces);
            Assert.False(moveablePieces[0].IsInPlay);
        }
    }
}
