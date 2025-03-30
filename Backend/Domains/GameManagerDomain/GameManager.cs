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

        public GameManager()
        {

        }
        
        public void CreateNewGame(int playerCount, int boardSize)
        {
            this.Board = new Board(boardSize);
            this.AddPlayers(playerCount);   
        }

        public void AddPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                this.Players.Add(new Player(i, (ColourEnum)((i % Enum.GetValues(typeof(ColourEnum)).Length) + 1), new List<Piece>()));
            }
        }

        public void RollForPlayerOrder()
        {
            Players.ForEach(player => player.LastRoll = Dice.Roll());
            Players.Sort((a, b) => a.LastRoll.CompareTo(b.LastRoll));

            while (true)
            {
                int highestRoll = Players[^1].LastRoll;
                var tiedPlayers = Players.Where(p => p.LastRoll == highestRoll).ToList();

                if (tiedPlayers.Count == 1)
                    break;

                tiedPlayers.ForEach(player => player.LastRoll = Dice.Roll());
                Players.Sort((a, b) => a.LastRoll.CompareTo(b.LastRoll)); 
            }
            this.CurrentPlayer = this.Players[^1]; 
        }



        public void Roll(int roll)
        {
            this.CurrentPlayer.IsTurn = true;
            this.CurrentPlayer.LastRoll = roll;
        }

        public List<Piece> GetPossibleMoves(int roll, Guid pieceId)
        {
            var availablePieces = Board.GetPossibleMoves(this.CurrentPlayer.GetPiecesInPlay());
            return availablePieces;
        }
        public PosIndex MovePiece(int pieceId)
        {
            var piece = CurrentPlayer.Pieces.FirstOrDefault(p => p.ID == pieceId);
            Board.MovePiece(piece, CurrentPlayer.LastRoll);
            return piece.PosIndex;
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

    public static class Dice
    {
        private static Random random = new Random();

        public static int Roll()
        {
            return random.Next(1, 7);
        }
    }

    public class Board
    {
        public Board(int boardsize)
        {

        }
    }

    
}
