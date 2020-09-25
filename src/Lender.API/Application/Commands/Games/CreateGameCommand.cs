using Lender.API.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Lender.API.Application.Commands
{
    public class CreateGameCommand : IRequest<GameDto>
    {
        public string Name { get; set; }

        public string Gender { get; set; }

        public IFormFile File { get; set; }
    }
}
