using MediatR;

namespace Lender.API.Application.Commands
{
    public class DeleteFriendCommand : IRequest
    {
        public long Id { get; set; }
    }
}
