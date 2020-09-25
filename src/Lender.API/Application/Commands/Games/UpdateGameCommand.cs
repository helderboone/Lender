using Lender.API.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Lender.API.Application.Commands
{
    public class UpdateGameCommand : IRequest<GameDto>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public IFormFile File { get; set; }
    }
}
