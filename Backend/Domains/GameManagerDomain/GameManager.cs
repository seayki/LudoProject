using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Backend.Services.BoardServices.Interfaces;
using Backend.Services.GameManagerServices;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.GameManagerDomain
{
    public class GameManager : IGameManagerService
    {
        public Board Board { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public Player CurrentPlayer { get; set; }
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

        public void AddPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                this.Players.Add(new Player(i, (ColourEnum)((i % Enum.GetValues(typeof(ColourEnum)).Length) + 1)));
            }
        }

        // Step 2 Roll for player order
        public int RollForPlayerOrder()
        {
            var playerOrder = gameSetupService.RollForPlayerOrder(Players);
            this.CurrentPlayer = Players[0];
            return CurrentPlayer.Id;
        }

        // Step 3 Player rolls dice
        public void Roll(int roll)
        {
            this.CurrentPlayer.IsTurn = true;
            this.CurrentPlayer.LastRoll = roll;
        }

        // Step 4 Return possible moves
        public List<int> GetPossibleMoves()
        {
            var availablePieces = Board.GetPossibleMoves(this.CurrentPlayer.GetPiecesInPlay());
            return availablePieces.Select(p => p.ID).ToList();
        }

        // Step 6 Move piece
        public List<Piece> MovePiece(int pieceId)
        {
            var affectedPieces = Board.MovePiece(pieceId, CurrentPlayer.LastRoll);
            return affectedPieces;
        }

        // Step 7 End turn
        public void NextTurn()
        {
            this.CurrentPlayer.IsTurn = false;
            this.CurrentPlayer.LastRoll = 0;
            int index = Players.IndexOf(CurrentPlayer);
            if (index == Players.Count - 1)
            {
                CurrentPlayer = Players[0];
            }
            else
            {
                CurrentPlayer = Players[index + 1];
            }
            if (CurrentPlayer.HasFinished())
            {
                NextTurn();
            }
        }
    } 
}
