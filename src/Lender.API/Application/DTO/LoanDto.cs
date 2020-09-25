using System;

namespace Lender.API.Application.DTO
{
    public class LoanDto
    {
        public long FriendId { get; set; }
        public string FriendName { get; set; }

        public long GameId { get; set; }
        public string GameName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
