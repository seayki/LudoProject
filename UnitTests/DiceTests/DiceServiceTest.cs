using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Services.DiceServices;
using FluentAssertions;

namespace UnitTests.DiceTests
{
    public class DiceServiceTest
    {
        private DiceService diceService = new DiceService();

        public void TestRoll()
        {
            var result = diceService.Roll();

            result.Should().BeGreaterThanOrEqualTo(1);
            result.Should().BeLessThanOrEqualTo(6);
        }
    }
}
