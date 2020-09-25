using System;

namespace Lender.API.Models
{
    public class Loan
    {
        public long FriendId { get; set; }
        public Friend Friend { get; set; }

        public long GameId { get; set; }
        public Game Game { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
