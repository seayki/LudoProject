using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;

namespace Backend.Services.PlayerServices.Interfaces
{
    public interface IPlayerService
    {
        //Player SelectPiece(int pieceId);
        Piece? SelectPiece(Player player, Guid pieceId);
        Piece GetBasePiece();
        bool AnyPiecesInPlay(Player player);

        List<Piece> GetPiecesInPlay(Player player);
        bool HasFinished(Player player);
    }
}
