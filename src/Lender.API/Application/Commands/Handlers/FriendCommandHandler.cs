using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Application.Notifify;
using Lender.API.Data;
using Lender.API.Helper;
using Lender.API.Models;
using Lender.API.Models.Base;
using Lender.API.Models.Validators;
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
        private readonly NotificationContext _notification;
        private readonly IEntityValidator _entityValidator;
        private readonly IMapper _mapper;

        public FriendCommandHandler(LenderContext context,
            UserManager<AppUser> userManager,
            IPhotoAccessor photoAccesor,
            NotificationContext notification,
            IEntityValidator entityValidator,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _photoAccesor = photoAccesor;
            _notification = notification;
            _entityValidator = entityValidator;
            _mapper = mapper;
        }

        public async Task<FriendDto> Handle(CreateFriendCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync("joao@email.com");

            var adress = new Address(request.Number, request.Street, request.Neighborhood, request.City);

            var friend = new Friend(request.Name, request.Email, request.Phone, adress, user);

            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            friend.AddPhoto(photoUploadResult);

            _entityValidator.Validate(new Entity[] { friend, adress });

            if (_notification.HasNotifications)
            {
                _photoAccesor.DeletePhoto(photoUploadResult?.PublicId);
                return null;
            }

            _context.Friends.Add(friend);

            await _context.Commit();

            return _mapper.Map<FriendDto>(friend);
        }

        public async Task<FriendDto> Handle(UpdateFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.Include(x => x.Address).FirstOrDefaultAsync(f => f.Id == request.Id);

            if (friend == null)
            {
                _notification.AddNotification("Friend", "Friend not found");
                return null;
            }

            _photoAccesor.DeletePhoto(friend.PhotoPublicId);

            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            var address = _mapper.Map<UpdateFriendCommand, Address>(request);

            friend.Update(request.Name, request.Email, request.Phone, address, photoUploadResult);

            _entityValidator.Validate(new Entity[] { friend, address });

            if (_notification.HasNotifications)
            {
                _photoAccesor.DeletePhoto(photoUploadResult?.PublicId);
                return null;
            }

            await _context.Commit();

            return _mapper.Map<FriendDto>(friend);
        }

        public async Task<Unit> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.FindAsync(request.Id);

            if (friend == null)
            {
                _notification.AddNotification("Friend", "Friend not found");
                return Unit.Value;
            }

            _photoAccesor.DeletePhoto(friend.PhotoPublicId);

            _context.Friends.Remove(friend);

            await _context.Commit();

            return Unit.Value;
        }
    }
}
