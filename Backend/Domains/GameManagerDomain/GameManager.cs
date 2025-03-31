using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.GameManagerDomain
{
    public class GameManager
    {
        public Board Board { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public Player CurrentPlayer { get; set; }
        public IGameSetupService GameSetupService { get; set; }
        

        public GameManager(IGameSetupService gameSetupService)
        {
            GameSetupService = gameSetupService;
        }
        
        public void CreateNewGame(int playerCount, int boardSize, int colorZoneLength)
        {
            this.AddPlayers(playerCount);
            var playerColours = Players.Select(p => p.Colour).ToList();
            this.Board = new Board(boardSize, colorZoneLength, playerColours);        
            Players = GameSetupService.RollForPlayerOrder(Players);
        }

        public void AddPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                this.Players.Add(new Player(i, (ColourEnum)((i % Enum.GetValues(typeof(ColourEnum)).Length) + 1)));
            }
        }

        public void Roll(int roll)
        {
            this.CurrentPlayer.IsTurn = true;
            this.CurrentPlayer.LastRoll = roll;
        }

        public List<Piece> GetPossibleMoves(int roll, Guid pieceId)
        {
            var availablePieces = Board.FindValidPiecesToMove(this.CurrentPlayer.GetPiecesInPlay());
            return availablePieces;
        }
        public Piece MovePiece(int pieceId)
        {
            var piece = CurrentPlayer.Pieces.FirstOrDefault(p => p.ID == pieceId);
            return Board.MovePiece(piece, CurrentPlayer.LastRoll);
        }
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

    public class Board
    {
        public Board(int boardsize)
        {

        }
    }

    
}
