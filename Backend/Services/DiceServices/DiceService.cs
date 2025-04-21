using Backend.Services.DiceServices.Interfaces;

namespace Backend.Services.DiceServices
{
    public class DiceService : IDiceService
    {
        private static readonly Random _random = new Random();

        public int Roll()
        {
            return _random.Next(1, 7); // Returns a number between 1 and 6
        }
    }
}
