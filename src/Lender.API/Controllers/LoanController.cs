using Lender.API.Application.Commands;
using Lender.API.Application.Queries.Loans;
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

        [HttpGet]
        public async Task<IActionResult> List()
        {
            return Ok(await Mediator.Send(new LoanListQuery()));
        }

        [HttpPut]
        public async Task<IActionResult> EndLoan(EndLoanCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
