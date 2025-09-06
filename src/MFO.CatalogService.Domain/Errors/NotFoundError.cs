using FluentResults;

namespace MFO.CatalogService.Domain.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    {
        Metadata.Add("ErrorType", "NotFound");
        Metadata.Add("StatusCode", 404);
    }
}