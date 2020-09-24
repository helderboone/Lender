using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Data;
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
        private readonly IMapper _mapper;

        public GameCommandHandler(LenderContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<GameDto> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = _mapper.Map<CreateGameCommand, Game>(request);

            var user = await _userManager.FindByEmailAsync("joao@email.com");

            game.User = user;

            _context.Games.Add(game);

            await _context.Commit();

            return _mapper.Map<GameDto>(game);
        }

        public async Task<GameDto> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.Id);

            game.Update(request.Name, request.Gender);

            await _context.Commit();

            return _mapper.Map<GameDto>(game);
        }

        public async Task<Unit> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var gane = await _context.Games.FindAsync(request.Id);

            _context.Games.Remove(gane);

            await _context.Commit();

            return Unit.Value;
        }
    }
}
