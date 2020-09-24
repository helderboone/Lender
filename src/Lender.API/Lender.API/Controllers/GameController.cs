using Lender.API.Application.Commands;
using Lender.API.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lender.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class GameController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            return Ok(await Mediator.Send(new GameDetailQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await Mediator.Send(new GameListQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriend(CreateGameCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFriend(UpdateGameCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriend(int id)
        {
            await Mediator.Send(new DeleteGameCommand { Id = id });

            return NoContent();
        }
    }
}
