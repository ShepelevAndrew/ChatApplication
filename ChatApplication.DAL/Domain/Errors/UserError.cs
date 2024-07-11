namespace ChatApplication.DAL.Domain.Errors;

public static class UserError
{
    public static NotFoundError NotFoundError => new("User");
}