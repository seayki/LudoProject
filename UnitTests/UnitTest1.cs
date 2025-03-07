namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Theory]
        // svare til int x = 1,2,3
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Test2(int x)
        {

        }
    }
}