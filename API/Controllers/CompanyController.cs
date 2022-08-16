

using System.Threading.Tasks;
using Application.AnnouncementCRUD;
using Application.AnnouncementCRUD.Dtos;
using Application.Company;
using Application.Company.Dtos;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = nameof(Roles.RolesEnum.Company))]
    public class CompanyController : Controller
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("EditProfile")]
        public async Task<ActionResult> EditProfile([FromBody] RequestProfileDto profile)
        {
            return HandleResult(await _mediator.Send(new EditProfile.Command
            {
                ProfileDto = profile
            }));
        }
    }
}
