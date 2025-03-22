using Common.Enums;

namespace UnitTests.BoardTests
{
    public class BoardServiceTests
    {

        public BoardServiceTests()
        {

        }

        [Theory]
        [InlineData(ColourEnum.Red)]
        [InlineData(ColourEnum.Green)]
        [InlineData(ColourEnum.Blue)]
        [InlineData(ColourEnum.Yellow)]
        public void Schould_get_one_posIndex_for_the_colour_start_tile(ColourEnum colour)
        {
            //Arange
            

            //Act
        

            //Assert
        }
    }
}
