using Backend.Domains.PieceDomain;
using Backend.Services.PieceService;
using Backend.Services.PieceService.Interfaces;
using Common.DTOs;
using Common.Enums;

namespace UnitTests.PieceTests
{
	public class PieceServiceTest
	{
		[Theory]
		[InlineData(ColourEnum.None, 42)]
		[InlineData(ColourEnum.Yellow, 12)]
		[InlineData(ColourEnum.None, 99999)]
		public void TestSuccessfulMovePieceMethod(ColourEnum posIndexColour, int posIndexIndex)
		{
			// Arrange
			var piece = new Piece(1, ColourEnum.Yellow);
			var posIndex = new PosIndex
			{
				Colour = posIndexColour,
				Index = posIndexIndex
			};

			IPieceService pieceService = new PieceService();

			// Act
			piece = pieceService.MovePiece(piece, posIndex);

			// Assert
			Assert.NotNull(piece.PosIndex);
			Assert.Equal(posIndexIndex, piece.PosIndex.Index);
			Assert.Equal(posIndexColour, piece.PosIndex.Colour);
		}

		[Theory]
		[InlineData("IsInPlay", false, typeof(Exception), "Piece is in base, not on board")]
		[InlineData("IsFinished", true, typeof(Exception), "Piece is in goal, and can't be moved anymore")]
		public void TestInvalidMovePieceMethod(string parameterCausingInvalidOperation, bool invalidValue, Type expectedExceptionType, string expectedMessage)
		{
			// Arrange
			var piece = new Piece(1, ColourEnum.Yellow);
			var posIndex = new PosIndex
			{
				Colour = ColourEnum.None,
				Index = 1
			};

			IPieceService pieceService = new PieceService();

			if (parameterCausingInvalidOperation == "IsInPlay")
			{
				piece.IsInPlay = invalidValue;
			}
			else if (parameterCausingInvalidOperation == "IsFinished")
			{
				piece.IsFinished = invalidValue;
			}

			// Act
			var exception = Record.Exception(() => pieceService.MovePiece(piece, posIndex));

			// Assert
			Assert.NotNull(exception);
			Assert.IsType(expectedExceptionType, exception);
			Assert.Equal(expectedMessage, exception.Message);
		}

		[Fact]
		public void TestSuccessfulMovePieceOut()
		{
			// Arrange
			var piece = new Piece(1, ColourEnum.Yellow);
			var posIndex = new PosIndex
			{
				Colour = ColourEnum.Yellow,
				Index = 1
			};

			IPieceService pieceService = new PieceService();

			// Act
			piece = pieceService.MovePieceOut(piece, posIndex);

			// Assert
			Assert.NotNull(piece.PosIndex);
			Assert.Equal(posIndex, piece.PosIndex);
			Assert.True(piece.IsInPlay);
		}
	}
}
