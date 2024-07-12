using System.Net;
using FluentResults;

namespace ChatApplication.DAL.Domain.Errors;

public class NoPermissionError : Error
{
    public NoPermissionError(string message)
        : base(message)
    {
        Metadata.Add(nameof(HttpStatusCode), HttpStatusCode.Forbidden);
    }

    public NoPermissionError()
        : this("There are no permissions to do the operation")
    {
    }
}