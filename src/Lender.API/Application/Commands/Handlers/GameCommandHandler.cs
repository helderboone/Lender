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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Commands
{
    public class GameCommandHandler :
        IRequestHandler<CreateGameCommand, GameDto>,
        IRequestHandler<UpdateGameCommand, GameDto>,
        IRequestHandler<DeleteGameCommand>
    {

        private readonly LenderContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IPhotoAccessor _photoAccesor;
        private readonly IEntityValidator _entityValidator;
        private readonly NotificationContext _notification;
        private readonly IMapper _mapper;

        public GameCommandHandler(LenderContext context,
            UserManager<AppUser> userManager,
            IPhotoAccessor photoAccesor,
            IEntityValidator entityValidator,
            NotificationContext notification, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _photoAccesor = photoAccesor;
            _entityValidator = entityValidator;
            _notification = notification;
            _mapper = mapper;
        }

        public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync("joao@email.com");

            var game = new Game(request.Name, request.Gender, user);

            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            game.AddPhoto(photoUploadResult);

            _entityValidator.Validate(new Entity[] { game });

            if (_notification.HasNotifications)
            {
                _photoAccesor.DeletePhoto(photoUploadResult?.PublicId);
                return null;
            }

            _context.Games.Add(game);

            await _context.Commit();

            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.Id);

            if (game == null)
            {
                _notification.AddNotification("Game", "Game not found");
                return null;
            }

            _photoAccesor.DeletePhoto(game.PhotoPublicId);

            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            game.Update(request.Name, request.Gender, photoUploadResult);

            _entityValidator.Validate(new Entity[] { game });

            if (_notification.HasNotifications)
            {
                _photoAccesor.DeletePhoto(photoUploadResult?.PublicId);
                return null;
            }

            await _context.Commit();

            return _mapper.Map<GameDto>(game);
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.Id);

            if (game == null)
            {
                _notification.AddNotification("Game", "Game not found");
                return Unit.Value;
            }

            bool isGameBorrowed = _context.Loans.Any(x => x.GameId == request.Id && x.EndDate == null);

            if (isGameBorrowed)
            {
                _notification.AddNotification("Game", "This game is borrowed. You must get back before delete it.");
                return Unit.Value;
            }

            _photoAccesor.DeletePhoto(game.PhotoPublicId);

            var loansGames = await _context.Loans.Where(x => x.GameId == request.Id).ToArrayAsync();

            _context.Games.Remove(game);

            _context.Loans.RemoveRange(loansGames);

            await _context.Commit();

            return Unit.Value;
        }
    }
}
