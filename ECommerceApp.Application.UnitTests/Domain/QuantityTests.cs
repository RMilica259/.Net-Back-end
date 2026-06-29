using ECommerceApp.Domain.ValueObjects;
using FluentAssertions;

namespace ECommerceApp.Application.UnitTests.Domain
{
    public class QuantityTests
    {
        [Fact]
        public void FromInt_WhenValueIsPositive_CreatesQuantity()
        {
            var quantity = Quantity.FromInt(5);

            quantity.Value.Should().Be(5);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void FromInt_WhenValueIsZeroOrNegative_ThrowsArgumentException(int value)
        {
            Action action = () => Quantity.FromInt(value);

            action.Should().Throw<ArgumentException>()
                .WithMessage("*Quantity must be greater than zero*");
        }

        [Fact]
        public void Add_WhenTwoQuantitiesAreAdded_ReturnsQuantityWithSum()
        {
            var first = Quantity.FromInt(2);
            var second = Quantity.FromInt(3);

            var result = first.Add(second);

            result.Value.Should().Be(5);
        }
    }
}