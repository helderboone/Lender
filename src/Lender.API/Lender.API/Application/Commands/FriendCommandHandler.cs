using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Data;
using Lender.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Commands
{
    public class FriendCommandHandler :
        IRequestHandler<CreateFriendCommand, FriendDto>,
        IRequestHandler<UpdateFriendCommand, FriendDto>,
        IRequestHandler<DeleteFriendCommand>
    {
        private readonly LenderContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public FriendCommandHandler(LenderContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<FriendDto> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = _mapper.Map<CreateFriendCommand, Friend>(request);

            var user = await _userManager.FindByEmailAsync("joao@email.com");

            friend.User = user;

            _context.Friends.Add(friend);

            var success = await _context.SaveChangesAsync() > 0;

            if (!success) throw new Exception("Problem saving changes");

            return _mapper.Map<FriendDto>(friend);
        }

        public async Task<FriendDto> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.FindAsync(request.Id);

            friend.Update(request.Name, request.Email, request.Phone);

            var success = await _context.SaveChangesAsync() > 0;

            if (!success) throw new Exception("Problem saving changes");

            return _mapper.Map<FriendDto>(friend);
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.FindAsync(request.Id);

            _context.Friends.Remove(friend);

            var success = await _context.SaveChangesAsync() > 0;

            if (!success) throw new Exception("Problem saving changes");

            return Unit.Value;
        }
    }
}
