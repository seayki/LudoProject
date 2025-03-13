using Backend.Domains.PlayerDomain;
using Common.Enums;
using Backend.Domains.PieceDomain;
using Common.DTOs;
using System.Security.Cryptography;

namespace UnitTests.PlayerTests
{

    public class PlayerDomainTest
    {
        public static IEnumerable<object[]> PlayerConstructorTestData()
        {
            yield return new object[] { 1, ColourEnum.Red, new List<Piece> {
                new Piece(1, ColourEnum.Red),
                new Piece(2, ColourEnum.Red),
                new Piece(3, ColourEnum.Red),
                new Piece(4, ColourEnum.Red)
            }, false, null, true };
            yield return new object[] { 2, ColourEnum.Blue, new List<Piece> {
                new Piece(1, ColourEnum.Blue),
                new Piece(2, ColourEnum.Blue),
                new Piece(3, ColourEnum.Blue),
                new Piece(4, ColourEnum.Blue)
            }, false, null, true };
            yield return new object[] { 3, ColourEnum.Green, new List<Piece> {
                new Piece(1, ColourEnum.Green),
                new Piece(2, ColourEnum.Green),
                new Piece(3, ColourEnum.Green),
                new Piece(4, ColourEnum.Green)
            }, false, null, true };
            yield return new object[] { 4, ColourEnum.Yellow, new List<Piece> {
                new Piece(1, ColourEnum.Yellow),
                new Piece(2, ColourEnum.Yellow),
                new Piece(3, ColourEnum.Yellow),
                new Piece(4, ColourEnum.Yellow),
            }, false, null, true };
        }

        [Theory]
        [MemberData(nameof(PlayerConstructorTestData))]
        public void TestPlayerConstructor(
            int id,
            ColourEnum colour,
            List<Piece> pieces,
            bool expectedIsTurn,
            PosIndex? expectedStartTile,
            bool shouldPass)
        {
            // Assert
            if (shouldPass)
            {
                var player = new Player(id, colour, pieces);
                Assert.NotNull(player);
                Assert.Equal(id, player.Id);
                Assert.Equal(colour, player.Colour);
                Assert.Equal(pieces, player.Pieces);
                Assert.Equal(expectedIsTurn, player.IsTurn);
                Assert.Equal(expectedStartTile, player.StartTile);
            }
            else
            {
                var ex = Assert.Throws<ArgumentException>(() => new Player(id, colour, pieces));
                Assert.Contains("A player must", ex.Message);
            }
        }
        [Fact]
        public void TestPlayer_InvalidColour()
        {
            // Arrange
            var pieces = new List<Piece>();

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => new Player(0, ColourEnum.None, pieces));
            Assert.Equal("A player must have a valid colour", exception.Message);
        }

        // Test om der er minimum 4 brikker for for en spiller
        [Fact]
        public void TestPlayer_InvalidNumberOfPieces()
        {
            var pieces = new List<Piece> { new Piece(1, ColourEnum.Red) };

            var exception = Assert.Throws<Exception>(() => new Player(1, ColourEnum.Red, pieces));
            Assert.Equal("A player must have 4 pieces", exception.Message);


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

            Assert.Equal(3, moveablePieces.Count);
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
