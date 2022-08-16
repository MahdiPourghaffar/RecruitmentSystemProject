using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Accounts.Dtos;
using Application.Common;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts
{
    public class Login
    {
        public class Command : IRequest<Result<UserDto>>
        {
            public LoginDto User { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<UserDto>>
        {
            private readonly SignInManager<User> _signInManager;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IJwtService _jwtService;

            public Handler(SignInManager<User> signInManager, IMapper mapper, UserManager<User> userManager, IJwtService jwtService)
            {
                _signInManager = signInManager;
                _mapper = mapper;
                _userManager = userManager;
                _jwtService = jwtService;
            }
            public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(request.User.UserName);
                    var result = await _signInManager.PasswordSignInAsync(user, request.User.Password, false, false);
                    
                    var token = await _jwtService.GenerateAsync(user);
                    if (result.Succeeded)
                    {
                        var userOut = _mapper.Map<UserDto>(user);
                        userOut.Token = token;
                        return Result<UserDto>.Success(userOut);
                    }
                    return Result<UserDto>.Failure("UserName Or Password InCorrecte :/");

                }
                catch (Exception e)
                {
                    return Result<UserDto>.Failure($"Failure : {e.Message}");
                }
            }
        }
    }
}
