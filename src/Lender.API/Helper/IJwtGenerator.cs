using Lender.API.Models;

namespace Lender.API.Helper
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser user);
    }
}
