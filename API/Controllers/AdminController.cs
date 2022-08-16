using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Admin;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = nameof(Roles.RolesEnum.Admin))]
        [HttpPut("ConfirmAnnouncement/{id}")]
        public async Task<ActionResult> ConfirmAnnouncement(int id)
        {
            return HandleResult(await _mediator.Send(new ConfirmAnnouncement.Command
            {
                AnnouncementId = id
            }));
        }
    }
}
