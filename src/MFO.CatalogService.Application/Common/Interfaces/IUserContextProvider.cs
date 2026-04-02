namespace MFO.CatalogService.Application.Common.Interfaces;

public interface IUserContextProvider
{
    string? UserId { get; }
}