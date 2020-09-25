﻿using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Commands
{
    public class EndLoanCommand : IRequest<LoanDto>
    {
        public long GameId { get; set; }

        public long FriendId { get; set; }
    }
}
