using Backend.Domains.PlayerDomain;
using Backend.Services.PlayerServices.Interfaces;

namespace Backend.Services.PlayerServices

{
    public class PlayerService : IPlayerService
    {
        Player IPlayerService.GetBasePiece()
        {
            throw new NotImplementedException();
        }

        Player IPlayerService.SelectPiece(int pieceId)
        {
            throw new NotImplementedException();
        }
    }
}
