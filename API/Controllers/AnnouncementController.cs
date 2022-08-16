using Application.AnnouncementCRUD;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.AnnouncementCRUD.Dtos;
using Application.Params;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Company")]
    public class AnnouncementController : Controller
    {
        private readonly IMediator _mediator;

        public AnnouncementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] AnnouncementRequestDto announcement)
        {
            return HandleResult(await _mediator.Send(new Create.Command
            {
                Announcement = announcement
            }));

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> List([FromQuery] AnnouncementParams param)
        {
            return HandleResult(await _mediator.Send(new List.Query
            {
                Params = param
            }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            return HandleResult(await _mediator.Send(new Detail.Query
            {
                AnnouncementId = id
            }));
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] AnnouncementRequestDto announcement)
        {
            return HandleResult(await _mediator.Send(new Update.Command
            {
                AnnouncementId = id,
                Announcement = announcement
            }));
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new Delete.Command
            {
                AnnouncementId = id
            }));
        }


    }
}
