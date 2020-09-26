using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Application.Notifify;
using Lender.API.Data;
using Lender.API.Models;
using Lender.API.Models.Base;
using Lender.API.Models.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Commands.Handlers
{
    public class LoanCommandHandler :
        IRequestHandler<CreateLoanCommand, LoanDto>,
        IRequestHandler<EndLoanCommand, LoanDto>
    {
        private readonly LenderContext _context;
        private readonly NotificationContext _notification;
        private readonly IEntityValidator _entityValidator;
        private readonly IMapper _mapper;

        public LoanCommandHandler(LenderContext context, NotificationContext notification, IEntityValidator entityValidator, IMapper mapper)
        {
            _context = context;
            _notification = notification;
            _entityValidator = entityValidator;
            _mapper = mapper;
        }

        public async Task<LoanDto> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.FindAsync(request.FriendId);

            if (friend == null)
            {
                _notification.AddNotification("Friend", "Friend not found");
                return null;
            }

            var game = await _context.Games.FindAsync(request.GameId);

            if (game == null)
            {
                _notification.AddNotification("Game", "Game not found");
                return null;
            }

            bool isGameBorrowed = _context.Loans.Any(x => x.GameId == request.GameId && x.EndDate == null);

            if (isGameBorrowed)
            {
                _notification.AddNotification("Game", "This game is borrowed. You must get back before to borrow it.");
                return null;
            }

            var loan = new Loan(friend, game);

            game.AddLoan(loan);
            friend.AddLoan(loan);

            _entityValidator.Validate(new Entity[] { loan });

            if (_notification.HasNotifications) return null;

            await _context.Commit();

            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<LoanDto> Handle(EndLoanCommand request, CancellationToken cancellationToken)
        {
            var loan = await _context.Loans
                .Include(l => l.Friend)
                .Include(l => l.Game)
                .FirstOrDefaultAsync(l => l.FriendId == request.FriendId && l.GameId == request.GameId);

            if (loan == null)
            {
                _notification.AddNotification("Loan", "This loan does not exist");
                return null;
            }

            loan.EndLoan();

            await _context.Commit();

            return _mapper.Map<LoanDto>(loan);
        }
    }
}
