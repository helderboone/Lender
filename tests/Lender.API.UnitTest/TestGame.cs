using FluentAssertions;
using Lender.API.Models;
using System;
using System.Linq;
using Xunit;

namespace Lender.API.UnitTest
{
    public class TestGame
    {
        private readonly AppUser _user;
        public TestGame()
        {
            _user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "joao",
                Email = "joao@email.com"
            };
        }

        [Fact]
        public void Test_InstanceGame_ShouldReturnValidGame()
        {
            var game = new Game("FIFA2020", "Sport", _user);
            game.Valid.Should().BeTrue();
        }

        [Fact]
        public void Test_InstanceGame_ShouldReturnInValidGame_WhenGameNoName()
        {
            var game = new Game(string.Empty, "Sport", _user);
            game.Valid.Should().BeFalse();
        }

        [Fact]
        public void Test_InstanceGame_ShouldReturnInValidGame_WhenGameNoGender()
        {
            var game = new Game("FIFA2020", string.Empty, _user);
            game.Valid.Should().BeFalse();
        }

        [Fact]
        public void Test_InstanceGame_ShouldReturnInValidGame_WhenNameExceedsMaxLength()
        {
            string name = string.Empty;

            for (int i = 0; i < 256; i++)
            {
                name += "a";
            }

            var game = new Game(name, "Sport", _user);
            game.Valid.Should().BeFalse();
            Assert.Contains("Name can has 255 caracters", game.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceGame_ShouldReturnInValidGame_WhenGenderExceedsMaxLength()
        {
            string gender = string.Empty;

            for (int i = 0; i < 51; i++)
            {
                gender += "a";
            }

            var game = new Game("FIFA2020", gender, _user);
            game.Valid.Should().BeFalse();
            Assert.Contains("Gender can has 50 caracters", game.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact]
        public void Test_Update_ShouldNotThrowExceptions_WhenPhotoIsNull()
        {
            var game = new Game("FIFA2020", string.Empty, _user);
            var exception = Record.Exception(() => game.Update("PES2020", "Sport", null));
            Assert.Null(exception);
        }

        [Fact]
        public void Test_AddPhoto_ShouldNotThrowExceptions_WhenPhotoIsNull()
        {
            var game = new Game("FIFA2020", string.Empty, _user);
            var exception = Record.Exception(() => game.AddPhoto(null));
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_AddLoan_ShouldReturnTrue(bool isLoanNull)
        {
            var game = new Game("FIFA2020", "Sport", _user);
            var address = new Address("12", "Av. Paulista", "Bela Vista", "São Paulo");
            var friend = new Friend("Marcos", "marcos@email.com", "01598988587", address, _user);
            var loan = isLoanNull ? null : new Loan(friend, game);
            game.AddLoan(loan);
            if (isLoanNull)
                Assert.Equal(0, game.Loans.Count);
            else
                Assert.Equal(1, game.Loans.Count);
        }


        [Fact]
        public void Test_AssociateUser_ShouldPopulateUser()
        {
            var game = new Game("FIFA2020", string.Empty, null);

            game.AssociateUser(_user);

            Assert.Same(_user, game.User);
        }

        [Fact]
        public void Test_InstanceGame_ShouldReturnInValidGame_WhenGameHasNoUser()
        {
            var game = new Game("FIFA2020", "Sport", null);
            game.Valid.Should().BeFalse();
            Assert.Contains("User is required", game.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

    }
}
