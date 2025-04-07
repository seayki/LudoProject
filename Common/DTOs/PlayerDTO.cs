using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTOs
{
	public class PlayerDTO
	{
		public int id { get; init; }
		public ColourEnum colour { get; init; }
		public PosIndex startTile { get; init; }
		public List<PieceDTO> pieces { get; init; }
	}
}
