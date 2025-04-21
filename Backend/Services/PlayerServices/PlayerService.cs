using Backend.Domains.PieceDomain;
using Backend.Domains.PlayerDomain;
using Backend.Services.PlayerServices.Interfaces;

namespace Backend.Services.PlayerServices

{
    public class PlayerService : IPlayerService
    {
        Piece IPlayerService.GetBasePiece()
        {
            throw new NotImplementedException();
        }

        //Player IPlayerService.SelectPiece(int pieceId)
        //{
        //    throw new NotImplementedException();
        //}

        public Piece SelectPiece(Player player, Guid pieceId)
        {
            var selectedPiece = player.Pieces.FirstOrDefault(p => p.ID == pieceId);

            return selectedPiece;
        }

        public bool AnyPiecesInPlay(Player player)
        {
            return player.Pieces.Any(p => p.IsInPlay);
        }

        public List<Piece> GetPiecesInPlay(Player player)
        {
            return player.Pieces.Where(p => p.IsInPlay).ToList();
        }

        public bool HasFinished(Player player)
        {
            var finishedPieces = player.Pieces.Where(p => p.IsFinished).ToList();
            return finishedPieces.Count == 4;
        }
    }
}
