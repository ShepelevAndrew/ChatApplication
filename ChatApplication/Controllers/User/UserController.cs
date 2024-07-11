using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.Controllers.User.Request;
using Microsoft.AspNetCore.Mvc;

namespace ChatApplication.Controllers.User;

[ApiController]
[Route("/api/user")]
public class UserController : ApiController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _userService.GetUser(userId);
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(UserRequest request)
    {
        var user = await _userService.CreateUser(request.Name);
        return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);
    }
}