using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Queries
{
    public class LoginQuery : IRequest<User>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
