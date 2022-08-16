using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Accounts.Dtos;
using Application.Common;
using AutoMapper;
using Domain;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts
{
    public class CompanySignUp
    {
        public class Command : IRequest<Result<UserDto>>
        {
            public SignUpDto User { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<UserDto>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public Handler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Result<UserDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = _mapper.Map<User>(request.User);
                    var result = await _userManager.CreateAsync(user, request.User.Password);
                    var result2 = await _userManager.AddToRoleAsync(user, nameof(Roles.RolesEnum.Company));
                    if (result.Succeeded && result2.Succeeded)
                    {
                        var userOut = _mapper.Map<UserDto>(user);
                        return Result<UserDto>.Success(userOut);
                    }

                    var errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += error.Description + Environment.NewLine;
                    }
                    return Result<UserDto>.Failure($"Failure {errorMessage}", HttpStatusCode.BadRequest);
                }
                catch (Exception e)
                {
                    return Result<UserDto>.Failure($"Failure : {e.Message}");
                }

            }
        }
    }
}
