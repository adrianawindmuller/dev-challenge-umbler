using Desafio.Umbler.Application.Common;
using Microsoft.AspNetCore.Mvc;

namespace Desafio.Umbler.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected new IActionResult Response(Result result)
        {
            if (result.ResponseType == ResultType.BadRequest)
            {
                return BadRequest(result.Message);
            }
            else if (result.ResponseType == ResultType.Created)
            {
                return Created("post", result.Data);
            }
            else if (result.ResponseType == ResultType.NoContent)
            {
                return NoContent();
            }
            else if (result.ResponseType == ResultType.NotFound)
            {
                return NotFound(result.Message);
            }
            else if (result.ResponseType == ResultType.Ok)
            {
                return result.Data is null ? NoContent() : Ok(result.Data);
            }

            return Ok();
        }
    }
}
