using ECommerceApp.Application.Services;
using FluentAssertions;

namespace ECommerceApp.Application.UnitTests.Services
{
    public class DiscountTests
    {
        [Theory]
        [InlineData("0641234560", 300)]
        [InlineData("0641234562", 200)]
        [InlineData("0641234564", 200)]
        [InlineData("0641234566", 200)]
        [InlineData("0641234568", 200)]
        [InlineData("0641234561", 100)]
        [InlineData("0641234563", 100)]
        [InlineData("0641234565", 100)]
        [InlineData("0641234567", 100)]
        [InlineData("0641234569", 100)]
        public void Calculate_WhenOrderIsBetween16And17_ReturnsDiscountBasedOnLastDigit(
            string phoneNumber,
            decimal expectedDiscount)
        {
            var discount = new Discount();

            var result = discount.Calculate(
                1000m,
                phoneNumber,
                new DateTime(2026, 6, 28, 16, 30, 0));

            result.Should().Be(expectedDiscount);
        }

        [Theory]
        [InlineData(15)]
        [InlineData(17)]
        [InlineData(18)]
        public void Calculate_WhenOrderIsOutsideDiscountPeriod_ReturnsZero(int hour)
        {
            var discount = new Discount();

            var result = discount.Calculate(
                1000m,
                "0641234560",
                new DateTime(2026, 6, 28, hour, 0, 0));

            result.Should().Be(0);
        }
    }
}
