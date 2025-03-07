namespace Backend.Services.PlayerServices.Interfaces
{
    public interface IPlayerService
    {
        Task<Piece> SelectPiece(int pieceId);
        Task<Piece> GetBasePiece();
    }
}
