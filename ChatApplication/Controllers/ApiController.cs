using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers;

public class ApiController : ControllerBase
{
    public IActionResult Problem(IList<IError> errors)
    {
        if (!errors.Any())
        {
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        var error = errors.First();
        return Problem(statusCode: (int)error.Metadata[nameof(HttpStatusCode)], detail: error.Message);
    }
}