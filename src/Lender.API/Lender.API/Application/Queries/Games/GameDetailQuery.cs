using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Queries
{
    public class GameDetailQuery : IRequest<GameDto>
    {
        public long Id { get; set; }
    }
}
