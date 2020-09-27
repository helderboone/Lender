using FluentAssertions;
using Lender.API.Models;
using System;
using System.Linq;
using Xunit;

namespace Lender.API.UnitTest
{
    public class TestLoan
    {
        private readonly Friend _friend;
        private readonly Game _game;
        private readonly AppUser _user;

        public TestLoan()
        {
            _game = new Game("Mortal Kombat", "Action", _user);
            var address = new Address("99", "Rua Vitoria", "Santo Antonio", "Vitoria");
            _friend = new Friend("Helder", "helder@email.com", "0212498734555", address, _user);
            _user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "joao",
                Email = "joao@email.com"
            };
        }

        [Fact]
        public void Test_InstanceLoan_ShouldReturnInvalidLoan_WhenGameIsNull()
        {
            var loan = new Loan(_friend, null);

            loan.Valid.Should().BeFalse();
            Assert.Contains("Game is required", loan.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_InstanceLoan_ShouldReturnInvalidLoan_WhenFriendIsNull()
        {
            var loan = new Loan(null, _game);

            loan.Valid.Should().BeFalse();
            Assert.Contains("Friend is required", loan.ValidationResult.Errors.Select(e => e.ErrorMessage));
        }

        [Fact]
        public void Test_AssociateGame_ShouldPopulateGame()
        {
            var loan = new Loan(null, null);

            loan.AssociateGame(_game);

            Assert.Same(_game, loan.Game);
        }

        [Fact]
        public void Test_AssociateFriend_ShouldPopulateFriend()
        {
            var loan = new Loan(null, null);

            loan.AssociateFriend(_friend);

            Assert.Same(_friend, loan.Friend);
        }

        [Fact]
        public void Test_EndLoan_ShouldPopulateEndDate()
        {
            var loan = new Loan(_friend, _game);

            loan.EndLoan();

            Assert.Equal(DateTime.Now.Date, loan.EndDate.Value.Date);
        }
    }
}
