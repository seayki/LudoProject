using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.PieceDomain
{
	public class Piece
	{
		public Piece(ColourEnum colour)
		{
			if (colour == ColourEnum.None)
			{
				throw new Exception("The piece must have a colour");
			}
			ID = Guid.NewGuid();
			Colour = colour;
			PosIndex = null;
			IsInPlay = false;
			IsFinished = false;
		}

		public Guid ID { get; }
		public ColourEnum Colour { get; }
		public PosIndex? PosIndex { get; set; }
		public bool IsInPlay { get; set; }
		public bool IsFinished { get; set; }
	}
}
