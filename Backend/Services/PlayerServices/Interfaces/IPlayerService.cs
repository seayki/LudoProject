using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;

namespace Backend.Services.PlayerServices.Interfaces
{
    public interface IPlayerService
    {
        //Player SelectPiece(int pieceId);
        Piece? SelectPiece(Player player, int pieceId);
        Player GetBasePiece();
    }
}
