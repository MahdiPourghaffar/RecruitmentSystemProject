using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts
{
    public class LogOut
    {
        public class Command : IRequest<Result>
        {

        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly SignInManager<User> _signInManager;

            public Handler(SignInManager<User> signInManager)
            {
                _signInManager = signInManager;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    // TODO : SignOut If LoggedIn

                    await _signInManager.SignOutAsync();
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
