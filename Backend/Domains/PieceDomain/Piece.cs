using Common.DTOs;
using Common.Enums;

namespace Backend.Domains.PieceDomain
{
	public class Piece
	{
		public Piece(int id, ColourEnum colour)
		{
			ID = id;
			Colour = colour;
			PosIndex = null;
			IsInPlay = false;
			IsFinished = false;
		}

		int ID { get; }
		ColourEnum Colour { get; }
		PosIndex? PosIndex { get; set; }
		bool IsInPlay { get; set; }
		bool IsFinished { get; set; }
	}
}
