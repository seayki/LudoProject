Feature: LudoController

The controller for ludo functionality

Scenario: The game is started with 4 players on a standard board
	Given I have 4 players
	And The board size is 52
	And The game backend is initialized
	When I call the StartGame endpoint
	Then I should recieve a list of 4 players
	And Each player should have pieces assigned
	And Each player should have a unique color and start tile

Scenario: The game is stared with 2 players on a small board
	Given I have 2 players
	And The board size is 36
	And The game backend is initialized
	When I call the StartGame endpoint
	Then I should recieve a list of 2 players
	And Each player should have pieces assigned
	And Each player should have a unique color and start tile

Scenario: Player rolls the die and gets valid moves
	Given I have 4 players
	And The board size is 52
    And the dice should always roll 4
	And The game backend is initialized
	And The game has started
	And First player has at least one piece that can be moved
    When the player rolls the die
    Then the response should contain a dice roll of 4
    And the response should contain a non-empty list of valid pieces
    And canReroll should be "false"