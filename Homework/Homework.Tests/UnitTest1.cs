using FluentAssertions;
using Xunit;

namespace Homework.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData("A", 5)]
        [InlineData("AB", 5)]
        [InlineData("BB", 5)]
        [InlineData("ABB", 7)]
        public void Deliver(string deliveries, int expectedTime)
        {
            var calculator = new TransportCalculator(deliveries);
            var result = calculator.Deliver();
            result.Should().Be(expectedTime);
        }
    }
}