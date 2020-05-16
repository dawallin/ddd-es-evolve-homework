using FluentAssertions;
using Xunit;

namespace Homework.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData(5, "A")]
        [InlineData(5, "AB")]
        [InlineData(5, "BB")]
        [InlineData(7, "ABB")]
        [InlineData(29, "AABABBAB")]
        [InlineData(29, "AAAABBBB")]
        [InlineData(49, "BBBBAAAA")]
        public void Deliver(int expectedTime, string deliveries)
        {
            var calculator = new TransportCalculator(deliveries);
            var result = calculator.Deliver();
            result.Should().Be(expectedTime);
        }
    }
}