using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Data;
using Lender.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Queries.Handlers
{
    public class GameQueryHandler :
        IRequestHandler<GameDetailQuery, GameDto>,
        IRequestHandler<GameListQuery, GameDto[]>,
        IRequestHandler<NotBorrowedGamesQuery, GameDto[]>
    {
        private readonly LenderContext _context;
        private readonly IMapper _mapper;

        public GameQueryHandler(LenderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GameDto> Handle(GameDetailQuery request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.Id);

            return _mapper.Map<Game, GameDto>(game);
        }

        public async Task<GameDto[]> Handle(GameListQuery request, CancellationToken cancellationToken)
        {
            var games = await _context.Games.ToArrayAsync();

            return _mapper.Map<Game[], GameDto[]>(games);
        }

        public async Task<GameDto[]> Handle(NotBorrowedGamesQuery request, CancellationToken cancellationToken)
        {
            var notBorrowedGames = await _context.Games.Where(x => x.Loans.Any(l => l.EndDate != null) && !x.Loans.Any()).ToArrayAsync();

            return _mapper.Map<Game[], GameDto[]>(notBorrowedGames);
        }
    }
}
