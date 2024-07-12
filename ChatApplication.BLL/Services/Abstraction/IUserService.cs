using ChatApplication.DAL.Domain;
using FluentResults;

namespace ChatApplication.BLL.Services.Abstraction;

public interface IUserService
{
    Task<Result<User>> GetUser(Guid userId);

    Task<User> CreateUser(string name);

    Task<Result> UpdateConnectionId(Guid userId, string connectionId);
}