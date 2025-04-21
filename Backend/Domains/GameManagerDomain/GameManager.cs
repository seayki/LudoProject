using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Backend.Services.BoardServices.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.GameManagerDomain
{
    public class GameManager : IGameManagerService
    {
        private Board Board { get; set; }
        private List<Player> Players { get; set; } = new List<Player>();
        private Player CurrentPlayer { get; set; }
        private int rollsTaken = 0;

        private readonly IBoardService boardService;
        private readonly IGameSetupService gameSetupService;
        private readonly IDiceService diceService;
 
        public GameManager(IBoardService boardService, IGameSetupService gameSetupService, IDiceService diceService)
        {
            this.boardService = boardService;
            this.gameSetupService = gameSetupService;
            this.diceService = diceService;
        }

        // Step 1 Create board
        public (Board Board, List<Player> Players) CreateNewGame(int playerCount, int boardSize, int lengthOfColourZone)
        {
            this.AddPlayers(playerCount);
            var colours = Players.Select(p => p.Colour).ToList();
            this.Board = new Board(boardSize, lengthOfColourZone, colours);          
            return (this.Board, this.Players);
        }

        private void AddPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                this.Players.Add(new Player(i, (ColourEnum)((i % Enum.GetValues(typeof(ColourEnum)).Length) + 1)));
            }
        }

        // Step 2 Roll for player order
        public List<Player> RollForPlayerOrder()
        {
            var playerOrder = gameSetupService.RollForPlayerOrder(Players);
            this.CurrentPlayer = Players[0];
            return playerOrder;
        }

        // Step 3 Player rolls dice
        public void Roll(int roll)
        {
            this.CurrentPlayer.LastRoll = roll;
            this.rollsTaken++;
        }

        // Step 4 Return possible moves
        public List<int>? GetMovablePieces()
        {
            var availablePieces = boardService.GetPossibleMoves(this.CurrentPlayer.GetPiecesInPlay());
            return availablePieces.Select(p => p.ID).ToList();
        }

        // Step 5 Move piece
        public List<Piece> MovePiece(int pieceId)
        {
            return boardService.MovePiece(pieceId, CurrentPlayer.LastRoll);
        }

        // Step 6 Check if current player can roll again due to 6'er rule or no pieces in play rule
        public bool CanRollAgain()
        {
            if (CurrentPlayer.LastRoll == 6)
                return true;

            if (!CurrentPlayer.AnyPiecesInPlay() && rollsTaken < 3)
                return true;

            return false;
        }

        // Step 7 End turn
        public int NextTurn()
        {
            // Move on to the next player and reset the roll
            int index = Players.IndexOf(CurrentPlayer);
            do
            {
                index = (index + 1) % Players.Count;
                CurrentPlayer = Players[index];
            } while (CurrentPlayer.HasFinished());

            rollsTaken = 0;
            this.CurrentPlayer.LastRoll = 0;

            return CurrentPlayer.Id;
        }
    } 
}
