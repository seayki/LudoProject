using Common.Enums;

namespace Common.DTOs
{
    public class PosIndex
    {
        private int Index { get; init; }
        private ColourEnum? Colour { get; init; }

        public PosIndex(int index, ColourEnum? colour)
        {
            Index = index;
            Colour = colour;
        }
    }
}
