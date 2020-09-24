using Lender.API.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lender.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class FriendController : BaseApiController
    {


        [HttpPut]
        public async Task<IActionResult> UpdateFriend(UpdateFriendCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> CreateFriend(CreateFriendCommand command)
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
