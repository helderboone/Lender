using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Commands
{
    public class UpdateFriendCommand : IRequest<FriendDto>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
