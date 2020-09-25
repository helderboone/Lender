using Lender.API.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lender.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LoanController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateLoan(CreateLoanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> EndLoan(EndLoanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
