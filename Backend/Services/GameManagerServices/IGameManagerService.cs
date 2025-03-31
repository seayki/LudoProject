using Backend.Domains.Board;
using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;

namespace Backend.Services.GameManagerServices
{
    public interface IGameManagerService
    {
        (Board Board, List<Player> Players) CreateNewGame(int playerCount, int boardSize, int lengthOfColourZone);
        void AddPlayers(int playerCount);
        int RollForPlayerOrder();
        void Roll(int roll);
        List<int> GetPossibleMoves();
        List<Piece> MovePiece(int pieceId);
        void NextTurn();
    }
}
