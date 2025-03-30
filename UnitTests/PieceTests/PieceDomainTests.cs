using Backend.Domains.PieceDomain;
using Common.DTOs;
using Common.Enums;
using FluentAssertions;

namespace UnitTests.PieceTests
{
	public class PieceDomainTests
	{
		[Theory]
		[InlineData(1, ColourEnum.Blue, null, false, false)]
		[InlineData(2, ColourEnum.Yellow, null, false, false)]
		[InlineData(3, ColourEnum.Green, null, false, false)]
		[InlineData(4, ColourEnum.Yellow, null, false, false)]
		public void TestPieceConstructor_successful(
			int id,
			ColourEnum colour,
			PosIndex? expectedPosIndex,
			bool expectedIsInPlay,
			bool expectedIsFinished)
		{
			// Arrange
			Piece? piece = null;

			// Act
			piece = new Piece(id, colour);

			// Assert
			piece.ID.Should().Be(id);
			piece.Colour.Should().Be(colour);
			piece.PosIndex.Should().Be(expectedPosIndex);
			piece.IsInPlay.Should().Be(expectedIsInPlay);
			piece.IsFinished.Should().Be(expectedIsFinished);
		}

		[Theory]
		[InlineData(1, ColourEnum.None)]
		public void TestPieceConstructor_failure(int id, ColourEnum colour)
		{
			// Arrange
			Piece? piece = null;

			// Act
			Action act = () => piece = new Piece(id, colour);

			// Assert
			piece.Should().BeNull();
			act.Should().Throw<Exception>().WithMessage("The piece must have a colour*");
		}
	}
}
