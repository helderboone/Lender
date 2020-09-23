﻿using Lender.API.Application.DTO;
using Lender.API.Helper;
using Lender.API.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Lender.API.Application.Queries
{
    public class LoginHandler : IRequestHandler<LoginQuery, User>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public LoginHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            //if (user == null) throw new RestException(HttpStatusCode.Unauthorized);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            //if (!result.Succeeded) throw new RestException(HttpStatusCode.Unauthorized);

            return new User
            {
                Token = _jwtGenerator.CreateToken(user),
                UserName = user.UserName,
            };
        }
    }
}
