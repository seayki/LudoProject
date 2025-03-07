using Common.DTOs;

namespace Backend.Domains.PlayerDomain
{
    public enum Colour
    {
        Yellow,
        Blue,
        Green,
        Red
    }

    public class Player
    {
        public int Id { get;}
        public Colour Colour { get; }
        public List<Piece> Pieces { get; }
        public bool IsTurn { get;}
        public PosIndex StartTile { get; }

        public Player(int id, Colour colour, List<Piece> pieces)
        {
            Id = id;
            Colour = colour;
            Pieces = pieces;
            IsTurn = false;
            StartTile = new PosIndex(0, 0);
        }

        public void SelectPiece(int pieceId)
        {
        }

        public void GetBasePiece()
        {

        }
    }
}
