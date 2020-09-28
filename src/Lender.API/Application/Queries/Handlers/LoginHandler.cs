using Lender.API.Application.DTO;
using Lender.API.Application.Notifify;
using Lender.API.Helper;
using Lender.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Queries
{
    public class LoginHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly NotificationContext _notification;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,NotificationContext notification , IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notification = notification;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                _notification.AddNotification("User", "Username or password incorrect");
                return null;
            }            

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
            {
                _notification.AddNotification("User", "Username or password incorrect");
                return null;
            }

            return new UserDto
            {
                Token = _jwtGenerator.CreateToken(user),
                UserName = user.UserName,
            };
        }
    }
}
