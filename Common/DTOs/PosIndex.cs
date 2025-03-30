using Common.Enums;

namespace Common.DTOs
{
    public class PosIndex
    {
        public int Index { get; init; }
        public ColourEnum? Colour { get; init; }

        public PosIndex(int index, ColourEnum? colour)
        {
            Index = index;
            Colour = colour;
        }
    }
}
