using MFO.CatalogService.Application.Common.Interfaces;

namespace MFO.CatalogService.Infrastructure.Services;

public class UserContextProvider : IUserContextProvider
{
    public string? UserId => "system";
}