using Backend.Controllers;
using Backend.Domains.Board;
using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Backend.Services.GameManagerServicesTemp.Interfaces;
using Common.DTOs;
using Common.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ControllerTest
{
	public class LudoControllerTest
	{
		private readonly Mock<ILogger<LudoController>> _loggerMock;
		private readonly Mock<IGameManagerService> _GameManagerMock;
		private readonly LudoController _sut; // SUT = System Under Test

		public LudoControllerTest()
		{
			_loggerMock = new Mock<ILogger<LudoController>>();
			_GameManagerMock = new Mock<IGameManagerService>();
			_sut = new LudoController(_GameManagerMock.Object, _loggerMock.Object);
		}

		[Theory]
		[MemberData(nameof(PossibleDiceValues.RangeNumbers), MemberType = typeof(PossibleDiceValues))]
		public void FindValidMoves_ValidInput_ReturnListOfPlayerIDs(int diceValue)
		{
			// arrange
			var validPieces = new List<int> { 1, 2, 3, 4 };
			_GameManagerMock.Setup(gm => gm.GetPossibleMoves()).Returns(validPieces);

			// act
			var result = _sut.FindValidMoves(diceValue).Result;

			// assert
			result.Should().BeOfType<OkObjectResult>()
				.And.BeEquivalentTo(new
				{
					StatusCode = 200,
					Value = validPieces
				}, options => options.ExcludingMissingMembers());
		}

		[Fact]
		public void FindValidMoves_GameManagerReturnsException_ErrorMessageReturned()
		{
			// arrange
			var validPieces = new List<int> { 1, 2, 3, 4 };
			_GameManagerMock.Setup(gm => gm.GetPossibleMoves()).Throws(new Exception());

			// act
			var result = _sut.FindValidMoves(6).Result;

			// assert
			result.Should().BeOfType<BadRequestObjectResult>()
				.Which.StatusCode.Should().Be(400);
		}

		[Theory]
		[MemberData(nameof(PieceAndPosIndexTestCases))]
		public void MoveSelectedPiece_VariousPiecesAndPosIndexes_ReturnListOfPiecesNewLocations(Piece piece, PosIndex posIndex)
		{
			// arrange
			var playerPieces = GetTestPieces(piece.Colour);
			playerPieces[playerPieces.FindIndex(p => p.ID == piece.ID)] = piece;

			_GameManagerMock.Setup(gm => gm.MovePiece(piece.ID)).Returns(() =>
			{
				var updatedPieces = playerPieces.Select(p =>
				{
					if (p.ID == piece.ID)
					{
						p.PosIndex = posIndex;
					}
					return p;
				}).ToList();

				return updatedPieces;
			});

			// act

			// assert

		}

		[Fact]
		public void MoveSelectedPiece_ChosenPieceDoesntExist_ReturnErrorMessage()
		{
			// arrange

			// act

			// assert

		}

		[Fact]
		public void moveSelectedPiece_GameManagerReturnsException_ErrorMessageReturned()
		{
			// arrange

			// act

			// assert

		}

		[Fact]
		public void StartGame_ValidInput_ListOfCreatedPlayersReturned()
		{
			// arrange
			var testPlayers = new List<Player>();
			testPlayers.Add(CreateTestPlayer(1, ColourEnum.Green));
			testPlayers.Add(CreateTestPlayer(2, ColourEnum.Yellow));
			_GameManagerMock.Setup(gm => gm.CreateNewGame(2, It.IsAny<int>(), It.IsAny<int>()))
				.Returns((new Board(), testPlayers));
			_GameManagerMock.Setup(gm => gm.RollForPlayerOrder()).Returns(1);

			// act
			var result = _sut.StartGame(2, 4).Result;

			// assert
			result.Should().BeOfType<OkObjectResult>()
				.Which.StatusCode.Should().Be(200);
			_GameManagerMock.Verify(s => s.CreateNewGame(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
			_GameManagerMock.Verify(s => s.RollForPlayerOrder(), Times.Once);
		}

		[Fact]
		public void StartGame_GameManagerReturnsException_ErrorMessageReturned()
		{
			// arrange
			_GameManagerMock.Setup(gm => gm.CreateNewGame(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
				.Throws(new Exception());

			// act
			var result = _sut.StartGame(2, 4).Result;

			// assert
			result.Should().BeOfType<BadRequestObjectResult>()
				.Which.StatusCode.Should().Be(400);
		}

		private List<Piece> GetTestPieces(ColourEnum colour)
		{
			var pieces = new List<Piece>();

			// Add 4 Pieces to the player's list
			for (int i = 0; i < 4; i++)
			{
				pieces.Add(new Piece(i, colour));
			}

			return pieces;
		}

		private Player CreateTestPlayer(int id, ColourEnum colour)
		{
			var pieces = GetTestPieces(colour);

			var player = new Player(id, colour, pieces);

			return player;
		}

		public class PossibleDiceValues
		{
			public static IEnumerable<object[]> RangeNumbers =>
				Enumerable.Range(1, 6)
						  .Select(n => new object[] { n });
		}

		public static IEnumerable<object[]> PieceAndPosIndexTestCases =>
		new List<object[]>
		{
			new object[] { new Piece(1, ColourEnum.Yellow), new PosIndex { Index = 1, Colour = ColourEnum.Yellow } },
			new object[] { new Piece(4, ColourEnum.Green), new PosIndex { Index = 2214, Colour = ColourEnum.Green } },
			new object[] { new Piece(3, ColourEnum.Yellow), new PosIndex { Index = 120, Colour = ColourEnum.None } },
		};
	}
}
