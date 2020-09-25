using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Data;
using Lender.API.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Commands.Handlers
{
    public class LoanCommandHandler : IRequestHandler<CreateLoanCommand, LoanDto>
    {
        private readonly LenderContext _context;
        private readonly IMapper _mapper;

        public LoanCommandHandler(LenderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.FindAsync(request.FriendId);

            var game = await _context.Games.FindAsync(request.GameId);

            var loan = new Loan
            {
                StartDate = DateTime.Now,

                Friend = friend,
                Game = game

            };

            game.Loans.Add(loan);
            friend.Loans.Add(loan);

            await _context.Commit();

            return _mapper.Map<LoanDto>(loan);
        }
    }
}
