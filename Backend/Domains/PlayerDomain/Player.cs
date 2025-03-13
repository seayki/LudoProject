using Backend.Domains.PieceDomain;
using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.PlayerDomain
{

    public class Player
    {
        public int Id { get; init; }
        public ColourEnum Colour { get; init; }
        public List<Piece> Pieces { get; init; }
        public bool IsTurn { get; set; }
        public PosIndex? StartTile { get; set; }

        public Player(int id, ColourEnum colour, List<Piece> pieces)
        {
            if (colour == ColourEnum.None)
            {
                throw new Exception("A player must have a valid colour");
            }
            if (pieces.Count != 4)
            {
                throw new Exception("A player must have 4 pieces");
            }

            Id = id;
            Colour = colour;
            Pieces = pieces;
            IsTurn = false;
            StartTile = null;
        }
    }
}
