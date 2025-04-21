using Backend.Domains.PieceDomain;
using Common.DTOs;
using Common.Enums;
using FluentAssertions;

namespace UnitTests.PieceTests
{
	public class PieceDomainTests
	{
		[Theory]
		[InlineData(ColourEnum.Blue, null, false, false)]
		[InlineData(ColourEnum.Yellow, null, false, false)]
		[InlineData(ColourEnum.Green, null, false, false)]
		public void TestPieceConstructor_successful(
			ColourEnum colour,
			PosIndex? expectedPosIndex,
			bool expectedIsInPlay,
			bool expectedIsFinished)
		{
			// Arrange
			Piece? piece = null;

			// Act
			piece = new Piece(colour);

			// Assert
			piece.Colour.Should().Be(colour);
			piece.PosIndex.Should().Be(expectedPosIndex);
			piece.IsInPlay.Should().Be(expectedIsInPlay);
			piece.IsFinished.Should().Be(expectedIsFinished);
		}

		[Theory]
		[InlineData(ColourEnum.None)]
		public void TestPieceConstructor_failure(ColourEnum colour)
		{
			// Arrange
			Piece? piece = null;

			// Act
			Action act = () => piece = new Piece(colour);

			// Assert
			piece.Should().BeNull();
			act.Should().Throw<Exception>().WithMessage("The piece must have a colour*");
		}
	}
}
