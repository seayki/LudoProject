using Backend.Domains.BoardDomain;
using Backend.Domains.GameManagerDomain;
using Backend.Domains.PlayerDomain;
using Backend.Services.GameRulesService;
using Backend.Services.GameSetupService.Interfaces;
using Backend.Services.PlayerServices.Interfaces;
using Common.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.GameManagerTest
{
    public class GameManagerTests
    {
        private readonly Mock<IGameSetupService> gameSetupServiceMock;
        private readonly Mock<IPlayerService> playerServiceMock;
        private readonly Mock<IGameRulesService> gameRulesServiceMock;
        private readonly GameManager gameManager;

        private readonly Player player1 = new Player(ColourEnum.Red) { Id = Guid.NewGuid() };
        private readonly Player player2 = new Player(ColourEnum.Blue) { Id = Guid.NewGuid() };
        private readonly Player player3 = new Player(ColourEnum.Green) { Id = Guid.NewGuid() };

        public GameManagerTests()
        {
            gameSetupServiceMock = new Mock<IGameSetupService>();
            playerServiceMock = new Mock<IPlayerService>();
            gameRulesServiceMock = new Mock<IGameRulesService>();
            gameManager = new GameManager(gameSetupServiceMock.Object, playerServiceMock.Object, gameRulesServiceMock.Object);

            var board = new Board(10, 2, new List<ColourEnum> { ColourEnum.Red, ColourEnum.Blue, ColourEnum.Green }, player1.Pieces);
            gameManager.CreateNewGame(3, 10, 2);
            typeof(GameManager)
                .GetProperty("Players", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(gameManager, new List<Player> { player1, player2, player3 });
            typeof(GameManager)
                .GetProperty("CurrentPlayer", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(gameManager, player1);
        }

        [Fact]
        public void NextTurn_SkipsFinishedPlayer()
        {
            playerServiceMock.Setup(p => p.HasFinished(player2)).Returns(true);
            playerServiceMock.Setup(p => p.HasFinished(player3)).Returns(false);

            var nextPlayerId = gameManager.NextTurn();

            Assert.Equal(player3.Id, nextPlayerId);
        }

        [Fact]
        public void NextTurn_WrapsAroundToFirstPlayer()
        {
            typeof(GameManager)
                .GetProperty("CurrentPlayer", BindingFlags.NonPublic | BindingFlags.Instance)!
                .SetValue(gameManager, player3);
            playerServiceMock.Setup(p => p.HasFinished(player1)).Returns(false);

            var nextPlayerId = gameManager.NextTurn();

            Assert.Equal(player1.Id, nextPlayerId);
        }

        [Fact]
        public void NextTurn_ResetsRoundState()
        {
            player1.LastRoll = 5;
            typeof(GameManager).GetField("rollsTaken", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(gameManager, 2);
            typeof(GameManager).GetField("movedPiece", BindingFlags.NonPublic | BindingFlags.Instance)!.SetValue(gameManager, true);
            playerServiceMock.Setup(p => p.HasFinished(player2)).Returns(false);

            gameManager.NextTurn();

            var rollsTakenValue = (int)typeof(GameManager).GetField("rollsTaken", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(gameManager)!;
            var movedPieceValue = (bool)typeof(GameManager).GetField("movedPiece", BindingFlags.NonPublic | BindingFlags.Instance)!.GetValue(gameManager)!;

            Assert.Equal(0, rollsTakenValue);
            Assert.False(movedPieceValue);
            Assert.Equal(0, player2.LastRoll);
        }
    }

}
