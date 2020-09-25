using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Data;
using Lender.API.Helper;
using Lender.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly IMapper _mapper;

        public GameCommandHandler(LenderContext context, UserManager<AppUser> userManager, IPhotoAccessor photoAccesor, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _photoAccesor = photoAccesor;
            _mapper = mapper;
        }


        public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            var game = _mapper.Map<CreateGameCommand, Game>(request);

            var user = await _userManager.FindByEmailAsync("joao@email.com");

            game.User = user;

            game.AddPhoto(photoUploadResult);

            _context.Games.Add(game);

            await _context.Commit();

            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.Id);

            _photoAccesor.DeletePhoto(game.PhotoPublicId);

            var photoUploadResult = _photoAccesor.AddPhoto(request.File);

            game.Update(request.Name, request.Gender, photoUploadResult);

            await _context.Commit();

            return _mapper.Map<GameDto>(game);
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.Id);

            _photoAccesor.DeletePhoto(game.PhotoPublicId);

            _context.Games.Remove(game);

            await _context.Commit();

            return Unit.Value;
        }
    }
}
