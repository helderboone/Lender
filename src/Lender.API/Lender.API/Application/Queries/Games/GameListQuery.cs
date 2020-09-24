using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Queries
{
    public class GameListQuery : IRequest<GameDto[]>
    {
    }
}
