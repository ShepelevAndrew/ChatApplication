using System.Net;
using FluentResults;

namespace ChatApplication.DAL.Domain.Errors;

public class NoPermissionError : Error
{
    public NoPermissionError()
        : base("There are no permissions to do the operation")
    {
        Metadata.Add(nameof(HttpStatusCode), HttpStatusCode.Forbidden);
    }
}