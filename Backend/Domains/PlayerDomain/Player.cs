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
            Id = id;
            Colour = colour;
            Pieces = pieces;
            IsTurn = false;
            StartTile = null;
        }
    }
}
