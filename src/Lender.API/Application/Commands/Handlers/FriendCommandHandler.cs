using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Data;
using Lender.API.Helper;
using Lender.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IPhotoAccessor _photoAccesor;
        private readonly IMapper _mapper;

        public FriendCommandHandler(LenderContext context, UserManager<AppUser> userManager, IPhotoAccessor photoAccesor, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _photoAccesor = photoAccesor;
            _mapper = mapper;
        }

        public async Task<FriendDto> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            var friend = _mapper.Map<CreateFriendCommand, Friend>(request);
            friend.Address = _mapper.Map<CreateFriendCommand, Address>(request);

            var user = await _userManager.FindByEmailAsync("joao@email.com");

            friend.User = user;

            friend.AddPhoto(photoUploadResult);

            _context.Friends.Add(friend);

            await _context.Commit();

            return _mapper.Map<FriendDto>(friend);
        }

        public async Task<FriendDto> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.Include(x => x.Address).FirstOrDefaultAsync(f => f.Id == request.Id);

            _photoAccesor.DeletePhoto(friend.PhotoPublicId);

            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            var address = _mapper.Map<UpdateFriendCommand, Address>(request);

            friend.Update(request.Name, request.Email, request.Phone, address, photoUploadResult);

            await _context.Commit();

            return _mapper.Map<FriendDto>(friend);
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.FindAsync(request.Id);

            _photoAccesor.DeletePhoto(friend.PhotoPublicId);

            _context.Friends.Remove(friend);

            await _context.Commit();

            return Unit.Value;
        }
    }
}
