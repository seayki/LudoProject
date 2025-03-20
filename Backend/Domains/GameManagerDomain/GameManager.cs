using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Common.Enums;

namespace Backend.Domains.GameManagerDomain
{
    public class GameManager
    {
        public Guid GameId { get; set; }  = new Guid();
        public Board board { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public GameManager()
        {

        }

        
        public void CreateNewGame(int playerCount, int boardSize)
        {
            this.board = new Board(boardSize);
            this.AddPlayers(playerCount);   
        }   

        public void AddPlayers(int playerCount)
        {
            for (int i = 0; i < playerCount; i++)
            {
                this.Players.Add(new Player(i, (ColourEnum)((i % Enum.GetValues(typeof(ColourEnum)).Length) + 1), new List<Piece>()));
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
