using Lender.API.Models.Base;
using Lender.API.Models.Validators;
using System;

namespace Lender.API.Models
{
    public class Loan : Entity
    {
        public Loan(Friend friend, Game game)
        {
            Friend = friend;
            Game = game;
            StartDate = DateTime.Now;
            Validar();
        }

        protected Loan() { }

        public long FriendId { get; private set; }
        public Friend Friend { get; private set; }

        public long GameId { get; private set; }
        public Game Game { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public void EndLoan()
        {
            EndDate = DateTime.Now;
            Validar();
        }

        public void AssociateGame(Game game)
        {
            Game = game;
            Validar();
        }

        public void AssociateFriend(Friend friend)
        {
            Friend = friend;
            Validar();
        }

        public override bool Validar()
        {
            return Validate(this, new LoanValidator());
        }
    }
}
