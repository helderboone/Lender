using Lender.API.Application.Queries.Loans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lender.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {
             
    }
}
