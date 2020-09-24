using MediatR;

namespace Lender.API.Application.Commands
{
    public class DeleteGameCommand : IRequest
    {
        public long Id { get; set; }
    }
}
