using System.Net;
using FluentResults;

namespace ChatApplication.DAL.Domain.Errors;

public class DuplicateError : Error
{
    public DuplicateError(string message)
        : base(message)
    {
        Metadata.Add(nameof(HttpStatusCode), HttpStatusCode.Conflict);
    }

    public DuplicateError(string entity, string storage)
        : this($"{entity} is already in {storage}.")
    {
    }
}