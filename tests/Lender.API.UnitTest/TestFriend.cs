using FluentAssertions;
using Lender.API.Helper;
using Lender.API.Models;
using System;
using System.Linq;
using Xunit;

namespace Lender.API.UnitTest
{
    public class TestFriend
    {
        private readonly Address _address;
        private readonly AppUser _user;
        private readonly PhotoUploadResult _photoUploadResult;

        public TestFriend()
        {
            _photoUploadResult = new PhotoUploadResult { PublicId = "83247zsdsad", Url = "www.cloudinary.com/imagem/1" };
            _address = new Address("10", "1234", "Eurico Salles", "Serra");
            _user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "joao",
                Email = "joao@email.com"
            };
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnInvalidFriend_WhenNameIsRequired()
        {
            var friend = new Friend("", "helder@email.com", "0112999842425", _address, _user);

            friend.Valid.Should().BeFalse();
            Assert.Contains("Name is required", friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnInvalidFriend_WhenNameExceedsMaxLength()
        {
            var name = string.Empty;

            for (int i = 0; i < 256; i++)
                name += "Test Name Length";

            var friend = new Friend(name, "helder@email.com", "0112999842425", _address, _user);

            friend.Valid.Should().BeFalse();
            Assert.Contains("The name max length is 255 characters",
                friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnInvalidFriend_WhenEmailIsEmpty()
        {
            var email = string.Empty;
            var friend = new Friend("Helder", email, "0112999842425", _address, _user);

            friend.Valid.Should().BeFalse();
            Assert.Contains("Email is required",
                friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnInvalidFriend_WhenEmailDoesntUseTheRightFormat()
        {
            var email = "This email does not use the right format";
            var friend = new Friend("Helder", email, "0112999842425", _address, _user);

            friend.Valid.Should().BeFalse();
            Assert.Contains("Email should use the following format example@email.com",
                friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnInvalidFriend_WhenPhoneIsEmpty()
        {
            var phone = string.Empty;
            var friend = new Friend("Helder", "helder@email.com", phone, _address, _user);

            friend.Valid.Should().BeFalse();
            Assert.Contains("Phone is required",
                friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnInvalidFriend_WhenPhoneExceedsMaxLength()
        {
            var phone = string.Empty;

            for (int i = 0; i < 256; i++)
                phone += "Test Phone Length";

            var friend = new Friend("Helder", "helder@email.com", phone, _address, _user);

            friend.Valid.Should().BeFalse();
            Assert.Contains("The phone max length is 255 characters",
                friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnInvalidFriend_WhenAddressIsNull()
        {
            Address address = null;

            var friend = new Friend("Helder", "helder@email.com", "0112999842425", address, _user);

            friend.Valid.Should().BeFalse();
            Assert.Contains("Address is required",
                friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceFriend_ShouldReturnValidFriend_WhenFriendIsPopulatedProperly()
        {
            var friend = new Friend("Helder", "helder@email.com", "0112999842425", _address, _user);

            friend.Valid.Should().BeTrue();
            Assert.Empty(friend.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_Update_ShouldNotThrowException_WhenPhotoIsNull()
        {
            var friend = new Friend("Helder", "helder@email.com", "0112999842425", _address, _user);
            var exception = Record.Exception(() => friend.Update("NewName", "newemail@mail.com", "0212799777799", _address, null));

            Assert.Null(exception);
        }

        [Fact]
        public void Test_Update_ShouldNotThrowException_WhenAddressIsNull()
        {
            var friend = new Friend("Helder", "helder@email.com", "0112999842425", _address, _user);
            var exception = Record.Exception(() => friend.Update("NewName", "newemail@mail.com", "0212799777799", null, _photoUploadResult));

            Assert.Null(exception);
        }

        [Fact]
        public void Test_AddPhoto_ShouldNotThrowException_WhenPhotoIsNull()
        {
            var friend = new Friend("Helder", "helder@email.com", "0112999842425", _address, _user);
            var exception =
                Record.Exception(() => friend.Update("NewName", "newemail@mail.com", "0212799777799", _address, null));

            Assert.Null(exception);
        }

        [Fact]
        public void Test_AssociateUser_ShouldPopulateUser()
        {
            var friend = new Friend("Helder", "helder@email.com", "0112999842425", _address, _user);

            friend.AssociateUser(_user);

            Assert.Same(_user, friend.User);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Test_AddLoan(bool isLoanNull)
        {
            var game = new Game("FIFA2020", "Sport", _user);
            var address = new Address("12", "Av. Paulista", "Bela Vista", "São Paulo");
            var friend = new Friend("Marcos", "marcos@email.com", "01598988587", address, _user);
            var loan = isLoanNull ? null : new Loan(friend, game);
            friend.AddLoan(loan);
            if (isLoanNull)
                Assert.Equal(0, friend.Loans.Count);
            else
                Assert.Equal(1, friend.Loans.Count);
        }
    }
}

