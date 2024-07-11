using System.Net;
using FluentResults;

namespace ChatApplication.DAL.Domain.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string entity)
        : base($"{entity} is not found.")
    {
        Metadata.Add(nameof(HttpStatusCode), HttpStatusCode.NotFound);
    }
}