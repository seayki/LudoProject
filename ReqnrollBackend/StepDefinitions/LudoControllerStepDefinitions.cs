using Backend.Controllers;
using Backend.Services.DiceServices.Interfaces;
using Backend.Services.GameManagerService;
using Common.DTOs;
using Common.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reqnroll;
using ReqnrollBackend.TestInfrastructure;
using ReqnrollBackend.TestInfrastructure.DiceFakes;
using System;
using FluentAssertions;

namespace ReqnrollBackend.StepDefinitions
{
    [Binding]
    public class LudoControllerStepDefinitions
    {
        private LudoBackendFactory _factory = new();
        private LudoController _controller;
        private IActionResult _response;
		private IDiceService? _diceService = null;
		private IServiceProvider _services;
		private IGameManagerService _gameManager;

		private int _playerNumber;
        private int _boardSize;

        [Given("I have {int} players")]
        public void GivenIHavePlayers(int numberOfPlayers)
        {
            _playerNumber = numberOfPlayers;
        }

        [Given("The board size is {int}")]
        public void GivenTheBoardSizeIs(int boardSize)
        {
            _boardSize = boardSize;
        }

		[Given("The game backend is initialized")]
		public void GivenTheGameBackendIsInitialized()
		{
			_factory = new LudoBackendFactory(_diceService);
			var scope = _factory.Services.CreateScope();
			_services = scope.ServiceProvider;

			_gameManager = _services.GetRequiredService<IGameManagerService>();
			var logger = _services.GetRequiredService<ILogger<LudoController>>();

			_controller = new LudoController(_gameManager, _diceService, logger);
		}


		[When("I call the StartGame endpoint")]
        public void WhenICallTheStartGameEndpoint()
        {
			_response = _controller.StartGame(_playerNumber, _boardSize).Result;
		}

        [Then("I should recieve a list of {int} players")]
        public void ThenIShouldRecieveAListOfPlayers(int expectedNumberOfPlayers)
        {
			var okResult = Assert.IsType<OkObjectResult>(_response);
			var players = Assert.IsAssignableFrom<IEnumerable<PlayerDTO>>(okResult.Value);

			Assert.Equal(expectedNumberOfPlayers, players.Count());
		}

        [Then("Each player should have pieces assigned")]
        public void ThenEachPlayerShouldHavePiecesAssigned()
        {
			var okResult = (OkObjectResult)_response;
			var players = (IEnumerable<PlayerDTO>)okResult.Value;

			Assert.All(players, p => Assert.NotEmpty(p.pieces));
		}

        [Then("Each player should have a unique color and start tile")]
        public void ThenEachPlayerShouldHaveAUniqueColorAndStartTile()
        {
			var okResult = (OkObjectResult)_response;
			var players = (IEnumerable<PlayerDTO>)okResult.Value;

			var colours = players.Select(p => p.colour).ToList();
			var startTiles = players.Select(p => p.startTile).ToList();

			Assert.Equal(colours.Count, colours.Distinct().Count());
			Assert.Equal(startTiles.Count, startTiles.Distinct().Count());
		}

		[Given("the dice should always roll {int}")]
		public void GivenTheDiceShouldAlwaysRoll(int roll)
		{
			IDiceService diceService = new SpecificValueDiceService(roll);

			int maxRoll = 6;
			var rollsForPlayerOrder = Enumerable.Range(maxRoll - _playerNumber + 1, _playerNumber)
												.Reverse();
			QueueBasedDiceService diceServiceWithOrderRolls = new QueueBasedDiceService(diceService);
			diceServiceWithOrderRolls.EnqueueRolls(rollsForPlayerOrder);
			_diceService = diceServiceWithOrderRolls;
		}

		[Given("The game has started")]
		public void GivenTheGameHasStarted()
		{
			_gameManager.CreateNewGame(_playerNumber, _boardSize, 6);
		}

		[Given("First player has at least one piece that can be moved")]
		public void GivenFirstPlayerHasAtLeastOnePieceThatCanBeMoved()
		{
			var player = _gameManager.GetCurrentPlayer();
			var piece = player.Pieces.First();
			piece.PosIndex = new(0, Common.Enums.ColourEnum.None); // or any index on the board that makes it movable
			piece.IsInPlay = true;
			piece.IsFinished = false;
		}

		[When("the player rolls the die")]
		public async Task WhenThePlayerRollsTheDie()
		{
			_response = await _controller.RollDieAndFindValidMoves();
		}

		[Then("the response should contain a dice roll of {int}")]
		public void ThenTheResponseShouldContainADiceRollOf(int expected)
		{
			var dto = (_response as OkObjectResult)?.Value as RollDieAndFindValidMovesResponseDTO;
			dto.Should().NotBeNull();
			dto.diceroll.Should().Be(expected);
		}

		[Then("the response should contain a non-empty list of valid pieces")]
		public void ThenTheResponseShouldContainANon_EmptyListOfValidPieces()
		{
			var dto = (_response as OkObjectResult)?.Value as RollDieAndFindValidMovesResponseDTO;
			dto.validPieces.Should().NotBeNullOrEmpty();
		}

		[Then("canReroll should be {string}")]
		public void ThenCanRerollShouldBe(string expected)
		{
			bool expectedBool = bool.Parse(expected);
			var dto = (_response as OkObjectResult)?.Value as RollDieAndFindValidMovesResponseDTO;
			dto.canReroll.Should().Be(expectedBool);
		}
	}
}
