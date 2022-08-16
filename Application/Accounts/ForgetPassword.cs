using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Accounts.Dtos;
using Application.Common;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts
{
    public class ForgetPassword
    {
        public class Command : IRequest<Result>
        {
            public ForgetPasswordDto User { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly UserManager<User> _userManager;
            private readonly IEmailService _emailService;

            public Handler(UserManager<User> userManager, IEmailService emailService)
            {
                _userManager = userManager;
                _emailService = emailService;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(request.User.Email);
                    if (user == null)
                    {
                        return Result.Failure("NotFound", HttpStatusCode.BadRequest);
                    }

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var emailBody = $"Your UserName : {user.UserName}    Your Reset Password Token : {token}";

                    await _emailService.Send(user.Email, emailBody, "Reset Password");

                    return Result.Success();
                }
                catch (Exception e)
                {
                    return Result.Failure($"Failure : {e.Message}");
                }
            }
        }
    }
}
