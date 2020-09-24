using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Queries
{
    public class FriendDetailQuery : IRequest<FriendDto>
    {
        public long Id { get; set; }
    }
}
