using Lender.API.Application.DTO;
using MediatR;

namespace Lender.API.Application.Commands
{
    public class UpdateGameCommand : IRequest<GameDto>
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }
    }
}
