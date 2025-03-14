using Backend.Domains.PlayerDomain;
using Common.Enums;
using Backend.Domains.PieceDomain;
using Common.DTOs;
using System.Security.Cryptography;
using FluentAssertions;

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

                player.Should().NotBeNull();
                player.Id.Should().Be(id);
                player.Colour.Should().Be(colour);
                player.Pieces.Should().BeEquivalentTo(pieces);
                player.IsTurn.Should().Be(expectedIsTurn);
                player.StartTile.Should().Be(expectedStartTile);
            }
            else
            {
                Action act = () => new Player(id, colour, pieces);
                act.Should().Throw<ArgumentException>()
                    .WithMessage("A player must");
            }
        }
        [Fact]
        public void TestPlayer_InvalidColour()
        {
            // Arrange
            var pieces = new List<Piece>();

            Action act = () => new Player(0, ColourEnum.None, pieces);

            act.Should().Throw<Exception>()
                .WithMessage("A player must have a valid colour");
        }

        // Test om der er minimum 4 brikker for for en spiller
        [Fact]
        public void TestPlayer_InvalidNumberOfPieces()
        {
            var pieces = new List<Piece> { new Piece(1, ColourEnum.Red) };

            Action act = () => new Player(1, ColourEnum.Red, pieces);

            act.Should().Throw<Exception>()
                .WithMessage("A player must have 4 pieces");
        }

        // Test om brikker kan flyttes ud
        [Fact]
        public void TestPlayerCanMovePieceOutFromStart()
        {
            var player = new Player(1, ColourEnum.Red, new List<Piece>
            {
                new Piece(1, ColourEnum.Red) { IsInPlay = false },
                new Piece(2, ColourEnum.Red) { IsInPlay = false },
                new Piece(3, ColourEnum.Red) { IsInPlay = false },
                new Piece(4, ColourEnum.Red) { IsInPlay = true }
            });

            var moveablePieces = player.Pieces.Where(p => !p.IsInPlay).ToList();

            moveablePieces.Should().HaveCount(3);
        }

        // Test at ingen brikker kan flyttes ud, hvis de er i spil.
        [Fact]
        public void TestPlayerCannotMoveAnyPieceOutFromStart()
        {
            var player = new Player(2, ColourEnum.Blue, new List<Piece>
            {
                new Piece(1, ColourEnum.Blue) { IsInPlay = true },
                new Piece(2, ColourEnum.Blue) { IsInPlay = true },
                new Piece(3, ColourEnum.Blue) { IsInPlay = true },
                new Piece(4, ColourEnum.Blue) { IsInPlay = true }
            });

            var moveablePieces = player.Pieces.Where(p => !p.IsInPlay).ToList();

            moveablePieces.Should().BeEmpty();
        }

        // Test for at sikre at kun en brik kan flyttes ud, hvis kun en brik ikke er i spil
        [Fact]
        public void TestPlayerHasOnePieceThatCanMoveOutFromStart()
        {
            var player = new Player(3, ColourEnum.Green, new List<Piece>
            {
                new Piece(1, ColourEnum.Green) { IsInPlay = true },
                new Piece(2, ColourEnum.Green) { IsInPlay = true },
                new Piece(3, ColourEnum.Green) { IsInPlay = true },
                new Piece(4, ColourEnum.Green) { IsInPlay = false }
            });

            var moveablePieces = player.Pieces.Where(p => !p.IsInPlay).ToList();

            moveablePieces.Should().ContainSingle()
                .Which.IsInPlay.Should().BeFalse();
        }
    }
}
