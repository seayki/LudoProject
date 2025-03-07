using Backend.Domains.PieceDomain;
using Common.DTOs;
using Common.Enums;

namespace UnitTests.PieceTests
{
	public class PieceDomainTests
	{
		[Theory]
		[InlineData(1, ColourEnum.Blue, null, false, false, true)]
		[InlineData(2, ColourEnum.Yellow, null, false, false, true)]
		[InlineData(3, ColourEnum.Green, null, false, false, true)]
		[InlineData(4, ColourEnum.Yellow, null, false, false, true)]
		[InlineData(1, ColourEnum.None, null, false, false, false)]
		public void TestPieceConstructor(
			int id,
			ColourEnum colour,
			PosIndex? expectedPosIndex,
			bool expectedIsInPlay,
			bool expectedIsFinished,
			bool shouldPass)
		{
			// Arrange
			Piece? piece = null;
			Exception? caughtException = null;

			// Act
			try
			{
				piece = new Piece(id, colour);
			}
			catch (Exception ex)
			{
				caughtException = ex;
			}

			// Assert
			if (shouldPass)
			{
				Assert.NotNull(piece);
				Assert.Null(caughtException);
				Assert.Equal(id, piece.ID);
				Assert.Equal(colour, piece.Colour);
				Assert.Equal(expectedPosIndex, piece.PosIndex);
				Assert.Equal(expectedIsInPlay, piece.IsInPlay);
				Assert.Equal(expectedIsFinished, piece.IsFinished);
			}
			else
			{
				Assert.Null(piece);
				Assert.NotNull(caughtException);
				Assert.IsType<Exception>(caughtException);
			}
		}
	}
}
