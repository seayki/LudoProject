using Common.Enums;

namespace Common.DTOs
{
    public class PlayerActionDTO
    {
        public ActionTypeEnum ActionTypeEnum { get; init; }
        public int? DiceValue { get; init; }
        public int PlayerId { get; init; }
        public Guid? PieceId { get; init; }
    }
}
