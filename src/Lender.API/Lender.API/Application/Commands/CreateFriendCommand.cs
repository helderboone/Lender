using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Commands
{
    public class CreateFriendCommand : IRequest<FriendDto>
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
