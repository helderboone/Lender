using Lender.API.Application.Commands;
using Lender.API.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lender.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FriendController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(long id)
        {
            return Ok(await Mediator.Send(new FriendDetailQuery { Id = id }));
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await Mediator.Send(new FriendListQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriend([FromForm] CreateFriendCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFriend([FromForm] UpdateFriendCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriend(int id)
        {
            await Mediator.Send(new DeleteFriendCommand { Id = id });

            return NoContent();
        }
    }
}
