using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.CategoryCRUD;
using System.Threading.Tasks;
using Application.CategoryCRUD.Dtos;
using Domain.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize(Roles = nameof(Roles.RolesEnum.Admin))]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> List()
        {
            return HandleResult(await _mediator.Send(new List.Query()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Details(int id)
        {
            return HandleResult(await _mediator.Send(new Detail.Query
            {
                CategoryId = id
            }));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryRequestDto category)
        {
            return HandleResult(await _mediator.Send(new Create.Command
            {
                Category = category
            }));
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] CategoryRequestDto category)
        {
            return HandleResult(await _mediator.Send(new Update.Command
            {
                CategoryId = id,
                Category = category
            }));
        }


        [HttpDelete("id")]
        public async Task<ActionResult> Delete(int id)
        {
            return HandleResult(await _mediator.Send(new Delete.Command
            {
                CategoryId = id
            }));
        }

    }
}
