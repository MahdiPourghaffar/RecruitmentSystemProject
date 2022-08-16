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
    public class ResetPassword
    {
        public class Command : IRequest<Result>
        {
            public ResetPasswordDto  ResetPasswordDto { get; set; }
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
                    var user = await _userManager.FindByNameAsync(request.ResetPasswordDto.UserName);
                    if (user == null)
                    {
                        return Result.Failure($"User NotFound");
                    }
                    var token = request.ResetPasswordDto.ResetPasswordToken;
                    var password = request.ResetPasswordDto.Password;
                    var result = await _userManager.ResetPasswordAsync(user, token, password);
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
