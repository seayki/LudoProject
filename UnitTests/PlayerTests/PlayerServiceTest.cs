using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Backend.Services.PlayerServices;
using Backend.Services.PlayerServices.Interfaces;
using Common.Enums;
using Xunit;
using FluentAssertions;

namespace UnitTests.PlayerTests
{
    public class PlayerServiceTest
    {
        private readonly PlayerService _playerService;

        public PlayerServiceTest()
        {
            _playerService = new PlayerService();
        }

        // Test om den korrekte brik returneres, når der vælges en brik
        [Fact]
        public void TestSelectPiece_ReturnsCorrectPiece()
        {
            // Arrange
            var player = new Player(1, ColourEnum.Red, new List<Piece>
        {
            new Piece(1, ColourEnum.Red),
            new Piece(2, ColourEnum.Red),
            new Piece(3, ColourEnum.Red),
            new Piece(4, ColourEnum.Red)
        });

            // Act
            var selectedPiece = _playerService.SelectPiece(player, 2);

            // Assert
            selectedPiece.Should().NotBeNull();
            selectedPiece.ID.Should().Be(2);
        }

        // Test om der retuneres null, hvis der vælges en brik, der ikke findes
        [Fact]
        public void TestSelectPiece_ReturnsNullForInvalidPieceId()
        {
            // Arrange
            var player = new Player(1, ColourEnum.Red, new List<Piece>
            {
                new Piece(1, ColourEnum.Red),
                new Piece(2, ColourEnum.Red),
                new Piece(3, ColourEnum.Red),
                new Piece(4, ColourEnum.Red)
            });

            // Act
            var selectedPiece = _playerService.SelectPiece(player, 999);

            // Assert
            selectedPiece.Should().BeNull();
        }

    }
}
