namespace Backend.Services.DiceService.Interfaces
{
	public interface IDiceService
	{
		int lastRoll { get; }
		int Roll();
	}
}
