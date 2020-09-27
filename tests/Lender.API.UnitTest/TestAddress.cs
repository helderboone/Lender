using FluentAssertions;
using Lender.API.Models;
using System.Linq;
using Xunit;

namespace Lender.API.UnitTest
{
    public class TestAddress
    {
        private readonly Address _address;

        public TestAddress()
        {
            _address = new Address("50", "Rua das Palmeiras", "Central Carapina", "Serra");
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnValidAddress()
        {
            var address = new Address("50", "Rua das Palmeiras", "Central Carapina", "Serra");

            address.Valid.Should().BeTrue();
            Assert.Empty(address.ValidationResult.Errors);
        }

        [Fact]
        public void Test_Update_ShouldUpdateValues()
        {
            var newAddress = new Address("50", "Rua das Palmeiras", "Central Carapina", "Serra");

            _address.Update(newAddress.Number, newAddress.Street, newAddress.Neighborhood, newAddress.City);

            _address.Valid.Should().BeTrue();
            Assert.Empty(_address.ValidationResult.Errors);
            Assert.Equal(_address.Number, newAddress.Number);
            Assert.Equal(_address.Street, newAddress.Street);
            Assert.Equal(_address.Neighborhood, newAddress.Neighborhood);
            Assert.Equal(_address.City, newAddress.City);
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenNumberIsEmpty()
        {
            var address = new Address(string.Empty, "Rua Santo Antonio", "Carapebus", "Serra");
            address.Valid.Should().BeFalse();

            Assert.Contains("Number is required", address.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenNumberExceedsMaxLength()
        {
            var number = string.Empty;

            for (int i = 0; i < 51; i++)
                number += "Number exceeds max length";

            var address = new Address(number, "Rua Santo Antonio", "Carapebus", "Serra");
            address.Valid.Should().BeFalse();
            Assert.Contains("The Number max length is 50 characters", address.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenStreetIsEmpty()
        {
            var address = new Address("55", string.Empty, "Carapebus", "Serra");
            address.Valid.Should().BeFalse();
            Assert.Contains("Street is required", address.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenStreetExceedsMaxLength()
        {
            var street = string.Empty;

            for (int i = 0; i < 101; i++)
                street += "Street exceeds max length";

            var address = new Address("55", street, "Carapebus", "Serra");
            address.Valid.Should().BeFalse();
            Assert.Contains("The Street max length is 100 characters", address.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenNeighborhoodIsEmpty()
        {
            var address = new Address("55", "Rua 22", string.Empty, "Serra");
            address.Valid.Should().BeFalse();
            Assert.Contains("Neighborhood is required", address.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenNeighborhoodExceedsMaxLength()
        {
            var neighborhood = string.Empty;

            for (int i = 0; i < 101; i++)
                neighborhood += "Neighborhood exceeds max length";

            var address = new Address("55", "Rua 22", neighborhood, "Serra");
            address.Valid.Should().BeFalse();
            Assert.Contains("The Neighborhood max length is 100 characters", address.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenCityIsEmpty()
        {
            var address = new Address("55", "Rua 22", "Eurico Salles", string.Empty);
            address.Valid.Should().BeFalse();
            Assert.Contains("City is required", address.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceAddress_ShouldReturnInvalidAddress_WhenCityExceedsMaxLength()
        {
            var city = string.Empty;

            for (int i = 0; i < 101; i++)
                city += "City exceeds max length";

            var address = new Address("55", "Rua 22", city, "Serra");
            address.Valid.Should().BeFalse();
            Assert.Contains("The Neighborhood max length is 100 characters", address.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }
    }
}
