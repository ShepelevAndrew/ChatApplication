using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.DAL.Domain;
using ChatApplication.DAL.Domain.Errors;
using ChatApplication.DAL.Persistent.Repositories.Abstraction;
using FluentResults;

namespace ChatApplication.BLL.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> GetUser(Guid userId)
    {
        var user = await _userRepository.GetUser(userId);
        if (user is null)
        {
            return Result.Fail(UserError.NotFoundError);
        }

        return user;
    }

    public async Task<User> CreateUser(string name)
    {
        var user = new User(name);
        await _userRepository.CreateUser(user);

        return user;
    }

    public async Task<Result> UpdateConnectionId(Guid userId, string connectionId)
    {
        var user = await _userRepository.GetUser(userId);
        if (user is null)
        {
            return Result.Fail(UserError.NotFoundError);
        }

        user.UpdateConnectionId(connectionId);
        await _userRepository.UpdateUser(user);

        return Result.Ok();
    }
}