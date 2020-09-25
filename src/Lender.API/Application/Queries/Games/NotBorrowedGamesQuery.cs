using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Queries
{
    public class NotBorrowedGamesQuery : IRequest<GameDto[]>
    {
    }
}
