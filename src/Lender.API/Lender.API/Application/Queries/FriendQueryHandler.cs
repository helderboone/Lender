using AutoMapper;
using Lender.API.Application.DTO;
using Lender.API.Data;
using Lender.API.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Queries
{
    public class FriendQueryHandler :
        IRequestHandler<FriendDetailQuery, FriendDto>,
        IRequestHandler<FriendListQuery, FriendDto[]>
    {
        private readonly LenderContext _context;
        private readonly IMapper _mapper;

        public FriendQueryHandler(LenderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FriendDto> Handle(FriendDetailQuery request, CancellationToken cancellationToken)
        {
            var friend = await _context.Friends.Include(f => f.Address).FirstOrDefaultAsync(f => f.Id == request.Id);

            return _mapper.Map<Friend, FriendDto>(friend);
        }

        public async Task<FriendDto[]> Handle(FriendListQuery request, CancellationToken cancellationToken)
        {
            var friends = await _context.Friends.Include(f => f.Address).ToArrayAsync();

            return _mapper.Map<Friend[], FriendDto[]>(friends);
        }
    }
}
