using System.Net;
using Application;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Application.Common;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    [Authorize]
    public class Controller : ControllerBase
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null)
            {
                return NotFound(new MainResponseDto(null, HttpStatusCode.NotFound, "NotFound"));
            }

            if (result.IsSuccess && result.Value != null)
            {
                return Ok(new MainResponseDto(result.Value, HttpStatusCode.OK, result.Message));
            }

            if (result.IsSuccess && result.Value == null)
            {
                return new JsonResult(new MainResponseDto(null, HttpStatusCode.NoContent, result.Message));
            }

            return new JsonResult(new MainResponseDto(result.Value, result.Status, result.Message));
        }

        protected ActionResult HandleResult(Result result)
        {
            if (result == null)
            {
                return NotFound(new MainResponseDto(null, HttpStatusCode.NotFound, "NotFound"));
            }

            if (result.IsSuccess)
            {
                return Ok(new MainResponseDto(null, HttpStatusCode.OK, result.Message));
            }

            return new JsonResult(new MainResponseDto(null, result.Status, result.Message));
        }
    }
}
