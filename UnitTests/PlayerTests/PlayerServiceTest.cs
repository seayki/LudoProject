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

namespace UnitTests.PlayerTests
{
    public class PlayerServiceTest
    {
        private readonly PlayerService _playerService;

        public PlayerServiceTest()
        {
            _playerService = new PlayerService();
        }

        [Fact]
        public void TestSelectPiece_ReturnsCorrectPiece()
        {
            // Arrange
            var player = new Player(1, ColourEnum.Red, new List<Piece>
        {
            new Piece(1, ColourEnum.Red),
            new Piece(2, ColourEnum.Red),
            new Piece(3, ColourEnum.Red)
        });

            // Act
            var selectedPiece = _playerService.SelectPiece(player, 2);

            // Assert
            Assert.NotNull(selectedPiece);
            Assert.Equal(2, selectedPiece.ID);
        }

    }
}
