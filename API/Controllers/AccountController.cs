using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Accounts;
using Application.Accounts.Dtos;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("CompanySignUp")]
        public async Task<ActionResult> CompanySignUp([FromBody] SignUpDto user)
        {
            return HandleResult(await _mediator.Send(new CompanySignUp.Command
            {
                User = user
            }));
        }

        [AllowAnonymous]
        [HttpPost("UserSignUp")]
        public async Task<ActionResult> UserSignUp([FromBody] SignUpDto user)
        {
            return HandleResult(await _mediator.Send(new UserSignUp.Command
            {
                User = user
            }));
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto user)
        {
            return HandleResult(await _mediator.Send(new Login.Command
            {
                User = user
            }));
        }


        [HttpPost("LogOut")]
        public async Task<ActionResult> LogOut()
        {
            return HandleResult(await _mediator.Send(new LogOut.Command()));
        }

        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword([FromBody] ForgetPasswordDto user)
        {
            return HandleResult(await _mediator.Send(new ForgetPassword.Command
            {
                User = user
            }));
        }
        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            return HandleResult(await _mediator.Send(new ResetPassword.Command
            {
                ResetPasswordDto = resetPasswordDto
            }));
        }

        [HttpPost("ChangePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            return HandleResult(await _mediator.Send(new ChangePassword.Command
            {
                ChangePasswordDto = changePasswordDto
            }));
        }
        [Authorize(Roles = nameof(Roles.RolesEnum.Admin))]
        [HttpGet("Claims")]
        public IEnumerable<ClaimDto> GetClaims()
        {
            var claims = User.Claims;
            var x = new List<ClaimDto>();
            foreach (var claim in claims)
            {
                x.Add(new ClaimDto
                {
                    Name = claim.Type,
                    Value = claim.Value
                });
            }
            return x;
        }

        [AllowAnonymous]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult> GetCurrentUser()
        {
            return HandleResult(await _mediator.Send(new CurrentUser.Query()));
        }

        public class ClaimDto
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
