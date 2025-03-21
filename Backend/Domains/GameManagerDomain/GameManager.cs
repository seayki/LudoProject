using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Common.Enums;

namespace Backend.Domains.GameManagerDomain
{
    public class GameManager
    {
        public Guid GameId { get; set; }  = new Guid();
        public Board Board { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public Player CurrentPlayer { get; set; }
        public Dice Dice { get; set; } = new();

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

        public void StartGame()
        {
            this.CurrentPlayer = this.Players[0];
        }

        public void RollForPlayerOrder()
        {
            Players.ForEach(player => player.LastRoll = Dice.Roll());

            Players.Sort(a => a.LastRoll);
            if (Players[0].LastRoll == Players[1].LastRoll)
            {
                RollForPlayerOrder();
            }
            StartGame();
        }

        public void EndTurn()
        {
            int index = Players.IndexOf(CurrentPlayer);
            if (index == Players.Count - 1)
            {
                CurrentPlayer = Players[0];
            }
            else
            {
                CurrentPlayer = Players[index + 1];
            }
            if (CurrentPlayer.IsFinished)
            {
                EndTurn();
            }
        }

        public void TakeTurn(int diceRoll)
        {
            
        }

    }

    public class Dice
    {
        private static Random random = new Random();

        public int Roll()
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
