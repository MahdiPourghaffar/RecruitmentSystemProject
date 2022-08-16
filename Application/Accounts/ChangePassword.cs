using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Accounts.Dtos;
using Application.Common;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts
{
    public class ChangePassword
    {
        public class Command : IRequest<Result>
        {
            public ChangePasswordDto ChangePasswordDto { get; set; }

        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly UserManager<User> _userManager;

            public Handler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userManager.FindByNameAsync(request.ChangePasswordDto.UserName);
                
                    var newPassword = request.ChangePasswordDto.NewPassword;
                    var oldPassword = request.ChangePasswordDto.OldPassword;

                    if (user.Password != oldPassword)
                    {
                        return Result.Failure("UserName Or Password Incorrect :/");
                    }

                    var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                    if (result.Succeeded)
                    {
                        return Result.Success();
                    }
                    var errorMessage = "";
                    foreach (var error in result.Errors)
                    {
                        errorMessage += error.Description + Environment.NewLine;
                    }
                    return Result.Failure($"Failure : {errorMessage}", HttpStatusCode.BadRequest);
                }
                catch (Exception e)
                {
                    return Result.Failure($"Failure : {e.Message}");
                }
                
            }
        }
    }
}
