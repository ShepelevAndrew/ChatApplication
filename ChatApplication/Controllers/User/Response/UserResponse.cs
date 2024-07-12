namespace ChatApplication.Controllers.User.Response;

public record UserResponse(
    Guid UserId,
    string Name)
{
    public static UserResponse ToUserResponse(DAL.Domain.User user)
    {
        return new UserResponse(user.UserId, user.Name);
    }
};