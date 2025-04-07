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
		public void RollForPlayerOrder_FourPlayersInList_SamePlayersComeOutInSpecificOrder(int[] expectedOrder, Queue<int> diceRolls)
		{
			// Arrange
			var mockDice = new Mock<IDiceService>();
			mockDice.Setup(d => d.Roll()).Returns(() => diceRolls.Dequeue());

			IGameSetupService gameSetupService = new GameSetupService(mockDice.Object);

			var players = new List<Player>
			{
				CreateTestPlayer(1, ColourEnum.Red),
				CreateTestPlayer(2, ColourEnum.Blue),
				CreateTestPlayer(3, ColourEnum.Green),
				CreateTestPlayer(4, ColourEnum.Yellow)
			};

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

		private Player CreateTestPlayer(int id, ColourEnum colour)
		{
			var pieces = new List<Piece>();

			// Add 4 Pieces to the player's list
			for (int i = 0; i < 4; i++)
			{
				pieces.Add(new Piece(colour));
			}

			var player = new Player(id, colour, pieces);

			return player;
		}

		public class PlayerOrderTestData : IEnumerable<object[]>
		{
			public IEnumerator<object[]> GetEnumerator()
			{
				yield return new object[] { new int[] { 2, 3, 4, 1 }, new Queue<int>(new[] { 1, 6, 3, 6, 4, 3 }) };
				yield return new object[] { new int[] { 1, 2, 3, 4 }, new Queue<int>(new[] { 6, 6, 6, 6, 4, 3, 2, 4, 2, 2, 2, 2, 2, 1 }) };
				yield return new object[] { new int[] { 4, 1, 2, 3 }, new Queue<int>(new[] { 1, 3, 3, 6 }) };
				yield return new object[] { new int[] { 2, 3, 4, 1 }, new Queue<int>(new[] { 1, 6, 3, 3 }) };

			}

			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		}
	}
}
