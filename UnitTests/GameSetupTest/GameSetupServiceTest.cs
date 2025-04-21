using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Backend.Services.DiceServices.Interfaces;
using Backend.Services.GameSetupService;
using Backend.Services.GameSetupService.Interfaces;
using Backend.Services.PieceService;
using Common.Enums;
using FluentAssertions;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.GameSetupTest
{
	public class GameSetupServiceTest
	{
		[Theory]
		[ClassData(typeof(PlayerOrderTestData))]
		public void RollForPlayerOrder_FourPlayersInList_SamePlayersComeOutInSpecificOrder(int[] expectedOrderIndexes, Queue<int> diceRolls)
		{
			// Arrange
			var mockDice = new Mock<IDiceService>();
			mockDice.Setup(d => d.Roll()).Returns(() => diceRolls.Dequeue());

			IGameSetupService gameSetupService = new GameSetupService(mockDice.Object);

			var players = new List<Player>
			{
				CreateTestPlayer(ColourEnum.Red),
				CreateTestPlayer(ColourEnum.Blue),
				CreateTestPlayer(ColourEnum.Green),
				CreateTestPlayer(ColourEnum.Yellow)
			};

			List<Guid> expectedOrder = new();
			foreach (var index in expectedOrderIndexes)
			{
				expectedOrder.Add(players[index].Id);
			}

			// Act
			var result = gameSetupService.RollForPlayerOrder(players);

			// Assert
			result.Should().HaveCount(4)
				.And.Subject.Select(p => p.Id).Should().Equal(expectedOrder);
		}

		[Fact]
		public void RollForPlayerOrder_ZeroPlayersInList_ThrowException()
		{
			// Arrange
			var mockDice = new Mock<IDiceService>();

			IGameSetupService gameSetupService = new GameSetupService(mockDice.Object);

			var players = new List<Player>();

			// Act
			Action act = () => gameSetupService.RollForPlayerOrder(players);

			// Assert
			act.Should().Throw<Exception>().WithMessage("*Need players to roll*");
		}

		private Player CreateTestPlayer(ColourEnum colour)
		{
			var player = new Player(colour);

			return player;
		}

		public class PlayerOrderTestData : IEnumerable<object[]>
		{
			public IEnumerator<object[]> GetEnumerator()
			{
				yield return new object[] { new int[] { 1, 2, 3, 0 }, new Queue<int>(new[] { 1, 6, 3, 6, 4, 3 }) };
				yield return new object[] { new int[] { 0, 1, 2, 3 }, new Queue<int>(new[] { 6, 6, 6, 6, 4, 3, 2, 4, 2, 2, 2, 2, 2, 1 }) };
				yield return new object[] { new int[] { 3, 0, 1, 2 }, new Queue<int>(new[] { 1, 3, 3, 6 }) };
				yield return new object[] { new int[] { 1, 2, 3, 0 }, new Queue<int>(new[] { 1, 6, 3, 3 }) };

			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}
