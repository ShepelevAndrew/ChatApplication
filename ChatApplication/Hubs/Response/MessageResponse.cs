using ChatApplication.Controllers.User.Response;
using ChatApplication.DAL.Domain;

namespace ChatApplication.Hubs.Response;

public record MessageResponse(
    UserResponse User,
    string Message);